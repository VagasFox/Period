using UnityEngine;
using System.Collections;

public class GravityGimmick : MonoBehaviour {
    private float gravity;

    GimmickStateTypeD GimStateTypeD;
	// Use this for initialization
	void Start () {
        GimStateTypeD = gameObject.GetComponent<GimmickStateTypeD>();
        gravity = (float)GimStateTypeD.gimmickNum;
	}
	
	// Update is called once per frame
	void Update () {
        gravity = (float)GimStateTypeD.gimmickNum;
        Physics.gravity = new Vector3(0, -gravity, 0);
	}
}
