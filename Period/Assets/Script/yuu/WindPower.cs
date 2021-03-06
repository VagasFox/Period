﻿using UnityEngine;
using System.Collections;
using Enum.Sound;

public class WindPower : MonoBehaviour {
    public float xWindPower;
    public float yWindPower;
    public float zWindPower;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider Coll)
    {
        if (Coll.tag == "Player")
        {
            SoundManager.PlaySE(SE_Enum.WIND, this.gameObject);
            Coll.GetComponent<RoboMove>().DirectMove.y += yWindPower * Time.deltaTime;
        }
        else
        {
            Coll.gameObject.transform.position += new Vector3(xWindPower, yWindPower, zWindPower) * Time.deltaTime;
        }
    }
}
