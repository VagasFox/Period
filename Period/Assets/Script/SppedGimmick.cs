using UnityEngine;
using System.Collections;

public class SppedGimmick : MonoBehaviour {
    [SerializeField]
    private GameObject enemyObj;
    private float speed;

    GimmickState gimState;
    // Use this for initialization
    void Start () {
        gimState = GetComponent<GimmickState>();
       
    }
	
	void Update () {
        speed = (float)gimState.gimmickNum;
        enemyObj.GetComponent<NavMeshAgent>().speed = speed;

    }
}
