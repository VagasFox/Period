using UnityEngine;
using System.Collections;

public class StartDirection : MonoBehaviour {
    GameObject Player;
    //bool St,Rot,SD;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        transform.parent = null;
        //transform.parent = GameObject.Find("Robo_Player_1027").transform;
        //St = false;
        //Rot = false;
        //SD = false;
	}
	
	// Update is called once per frame
	void Update () {
        //if (St == false)
	}
}
