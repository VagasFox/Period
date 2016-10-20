using UnityEngine;
using System.Collections;

public class GravityGimmick : MonoBehaviour {
    public float gravity;

    GimmickStateTypeD GimStateTypeD;
	// Use this for initialization
	void Start () {
        GimStateTypeD = gameObject.GetComponent<GimmickStateTypeD>();
        gravity = (float)GimStateTypeD.gimmickNum;
        GimStateTypeD.Gravity = true;
        GimStateTypeD.Door = false;
        GimStateTypeD.Rotation = false;
	}
	
	// Update is called once per frame
	void Update () {
        gravity = (float)GimStateTypeD.gimmickNum;
        Physics.gravity = new Vector3(0, -gravity, 0);
	}
}
