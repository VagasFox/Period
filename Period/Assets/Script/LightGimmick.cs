using UnityEngine;
using System.Collections;

public class LightGimmick : MonoBehaviour {
    [SerializeField] private GameObject LightObj;
    private float light_power;

    GimmickStateTypeD GimStateTypeD;
    // Use this for initialization
    void Start() {
        GimStateTypeD = gameObject.GetComponent<GimmickStateTypeD>();
        GimStateTypeD.Light = true;
        GimStateTypeD.Door = false;
        GimStateTypeD.Gravity = false;
        GimStateTypeD.Rotation = false;
    }
	
	// Update is called once per frame
	void Update () {
        light_power = (float)GimStateTypeD.gimmickNum;
        LightObj.GetComponent<Light>().intensity = light_power;
	}
}
