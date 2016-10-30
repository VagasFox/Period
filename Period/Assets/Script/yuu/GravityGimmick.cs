using UnityEngine;
using System.Collections;

public class GravityGimmick : MonoBehaviour {
    private float gravity;
    
    
    //PlayertagのRoboMoveの重力操作用
    RoboMove PLGravity;
    GimmickState gimState;
	// Use this for initialization
	void Start () {
        gimState = GetComponent<GimmickState>();
        gravity = (float)gimState.gimmickNum;
        PLGravity = GameObject.FindGameObjectWithTag("Player").GetComponent<RoboMove>();
	}
	
	// Update is called once per frame
	void Update () {
        gravity = (float)gimState.gimmickNum;
        Physics.gravity = new Vector3(0, -gravity, 0);
        PLGravity.gravity = gravity;
	}
}
