using UnityEngine;
using System.Collections;

public class DoorGimmick : MonoBehaviour {
    public GameObject LeftDoor;
    public GameObject RightDoor;
    
    public double minPassNum = 0;
    public double maxPassNum = 10;
    public decimal password;
    public decimal minPass;
    public decimal maxPass;
    private float leftDoorMove;
    private float rightDoorMove;


    public bool Complete;

    GimmickStateTypeD GimStateTypeD;
	// Use this for initialization
	void Start () {
        GimStateTypeD = gameObject.GetComponent<GimmickStateTypeD>();
        minPass = (decimal)minPassNum;
        maxPass = (decimal)maxPassNum;
        Complete = false;
        leftDoorMove = LeftDoor.transform.position.x;
        rightDoorMove = RightDoor.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
        NumGimmick();
        OpenDoor();
	}

    void NumGimmick()
    {
        if (gameObject.tag == "Gimmick")
        {
            password = GimStateTypeD.gimmickNum;

            if (minPass < password && password < maxPass)
            {
                Complete = true;
            }
        }
    }

    void OpenDoor()
    {
        if (Complete == true)
        {
            LeftDoor.transform.position += new Vector3(leftDoorMove * Time.deltaTime, 0, 0);
            RightDoor.transform.position += new Vector3(rightDoorMove * Time.deltaTime, 0, 0);
        }
    }
}
