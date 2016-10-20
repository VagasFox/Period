using UnityEngine;
using System.Collections;

public class RotationGimmick : MonoBehaviour {
    public GameObject Bridge;
    public float rotation;

    GimmickStateTypeD GimStateTypeD;
	// Use this for initialization
	void Start () {
	    GimStateTypeD = gameObject.GetComponent<GimmickStateTypeD>();
        GimStateTypeD.Rotation = true;
        GimStateTypeD.Gravity = false;
        GimStateTypeD.Door = false;
	}
	
	// Update is called once per frame
	void Update () {
        rotation = (float)GimStateTypeD.gimmickNum;
        Bridge.transform.rotation = Quaternion.Euler(0, rotation, 0);
	}
}
