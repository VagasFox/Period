using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public Transform[] wayPoints;
    public int currentRoot;

    private Ray ray;
    private RaycastHit rayHit;
    public bool isDiscovery = false;
    public GameObject target;

    float time;
    public float distancePoint = 5f;

    void Start() {
        
    }

    void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = target.transform.position;
        //Patrol();
        //SearchTarget();

        //if (!isDiscovery) Patrol();
        //else              Tracking();        
    }

    void SearchTarget()
    {
        ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 20.0f);

        if (Physics.Raycast(ray, out rayHit, 50.0f))
        {
            if (rayHit.collider.CompareTag("Player")) {
                target = rayHit.collider.gameObject;
                isDiscovery = true;
            }
        }
    }

    //巡回
    void Patrol() {
        Vector3 pos = wayPoints[currentRoot].position;

        if (Vector3.Distance(transform.position, pos) < 0.5f)
        {
            currentRoot = (currentRoot < wayPoints.Length - 1) ? currentRoot + 1 : 0;
        }

        GetComponent<NavMeshAgent>().SetDestination(pos);
    }

    //追跡
    void Tracking() {
        GetComponent<NavMeshAgent>().SetDestination(target.transform.position);


        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance <= distancePoint || target.name == "Robo_Player_1027")
        {
            time += Time.deltaTime;
            if (time > 3f)
            {
                time = 0f;
                isDiscovery = false;
            }
        }
        else {
            time = 0f;
        }
    }
}
