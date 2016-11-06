using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public Transform[] wayPoints;                   //通る道
    public int currentRoot;                         //通る道の番号
    NavMeshAgent agent;                             //EnemyについているNavMeshAgent
    private Ray ray;                                //プレイヤー探索用のRay
    private RaycastHit rayHit = new RaycastHit();   //プレイヤー探索用のRaycastHit
    private GameObject target;                      //追いかけるターゲット

    [SerializeField]
    private float rayDistance = 30f;                //Rayの長さ
    private float trackingCount = 0;    

    [SerializeField]
    private float giveUpCount = 10f;                //追跡をあきらめるまでの秒数
    [SerializeField]
    private float distancePoint = 5f;               //追跡を諦める距離

    public enum EnemyState {
        Patrol, //巡回
        Chase   //追跡
    }
    EnemyState state = EnemyState.Patrol;
    EnemyState nextState;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        switch (state)
        {
            case EnemyState.Patrol:
                Patroling();
                break;
            case EnemyState.Chase:
                Chasing();
                break;
        }

        if (state != nextState)
        {
            state = nextState;
            switch (state)
            {
                case EnemyState.Patrol:
                    Patroling();
                    break;
                case EnemyState.Chase:
                    Chasing();
                    break;

            }
        }

        SearchTarget();
    }

    void SearchTarget()
    {
        ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 20.0f);

        if (Physics.Raycast(ray, out rayHit, rayDistance))
        {
            if (rayHit.collider.CompareTag("Player"))
            {
                target = rayHit.collider.gameObject;
                ChangeState(EnemyState.Chase);
            }

            if (state == EnemyState.Chase &&
                !rayHit.collider.CompareTag("Player"))
            {
                if (trackingCount < 10f)
                {
                    trackingCount += Time.deltaTime;

                }
                else
                {
                    ChangeState(EnemyState.Patrol);
                    trackingCount = 0;
                }
            }
        }
    }

    /// <summary>
    /// EnemyStateの変更
    /// </summary>
    /// <param name="next"></param>
    void ChangeState(EnemyState next)
    {
        this.nextState = next;
    }

    /// <summary>
    /// 巡回
    /// </summary>
    void Patroling() {
        Vector3 pos = wayPoints[currentRoot].position;

        if (Vector3.Distance(transform.position, pos) < 0.5f)
        {
            currentRoot = (currentRoot < wayPoints.Length - 1) ? currentRoot + 1 : 0;
        }

        GetComponent<NavMeshAgent>().SetDestination(pos);
    }

    /// <summary>
    /// 追跡
    /// </summary>
    void Chasing()
    {
        Vector3 targetPos = target.transform.position;

        agent.SetDestination(targetPos);

        if (Vector3.Distance(transform.position, targetPos) > distancePoint)
        {
            ChangeState(EnemyState.Patrol);
            trackingCount = 0f;
        }
    }
}
