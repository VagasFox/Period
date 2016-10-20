using UnityEngine;
using System.Collections;

public class GravityGimmick : MonoBehaviour {
    private float gravity;

    GimmickState gimState;
	// Use this for initialization
	void Start () {
        gimState = gameObject.GetComponent<GimmickState>();
        gravity = (float)gimState.gimmickNum;
	}
	
	// Update is called once per frame
	void Update () {
        gravity = (float)gimState.gimmickNum;
        Physics.gravity = new Vector3(0, -gravity, 0);
	}
}
