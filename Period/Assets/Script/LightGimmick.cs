using UnityEngine;
using System.Collections;

public class LightGimmick : MonoBehaviour {
    [SerializeField] private GameObject LightObj;
    private float light_power;

    GimmickState gimState;
    // Use this for initialization
    void Start() {
        gimState = gameObject.GetComponent<GimmickState>();
    }
	
	// Update is called once per frame
	void Update () {
        light_power = (float)gimState.gimmickNum;
        LightObj.GetComponent<Light>().intensity = light_power;
	}
}
