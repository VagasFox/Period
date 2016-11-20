using UnityEngine;
using System.Collections;

public class DoorGimmick : MonoBehaviour {
    [SerializeField] private GameObject LeftDoor;
    [SerializeField] private GameObject RightDoor;
    
    public double minPassNum = 0;
    public double maxPassNum = 10;
    public decimal password;
    public decimal minPass;
    public decimal maxPass;
    private float leftDoorMove;
    private float rightDoorMove;
    public bool Complete;

    private float movePoint = 9f;
    private float moveTime;
    Vector3 firstPosL, firstPosR;

    GimmickState gimState;
	// Use this for initialization
	void Start () {
        gimState = GetComponent<GimmickState>();

        minPass = (decimal)minPassNum;
        maxPass = (decimal)maxPassNum;
        Complete = false;

        firstPosL = LeftDoor.transform.position;
        firstPosR = RightDoor.transform.position;
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
            password = gimState.gimmickNum;

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
            Vector3 endPosL = new Vector3(firstPosL.x - movePoint, firstPosL.y, firstPosL.z);
            Vector3 endPosR = new Vector3(firstPosR.x + movePoint, firstPosR.y, firstPosR.z);

            moveTime += Time.deltaTime;
            LeftDoor.transform.position = Vector3.Lerp(firstPosL, endPosL, moveTime);
            RightDoor.transform.position = Vector3.Lerp(firstPosR,endPosR, moveTime);
        }
    }
}