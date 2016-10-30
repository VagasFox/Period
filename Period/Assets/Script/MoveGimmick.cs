using UnityEngine;
using System.Collections;

public class MoveGimmick : MonoBehaviour {
    GimmickState gimState;
    [SerializeField] private float standingNum;
    private decimal standingNumber;

    [SerializeField] private bool endless = false;
    private bool moving = false;

    private Vector3 startPos;
    [SerializeField] private Transform endPos;
    bool checkMove = false;
    int moveCount;
    float moveTime = 0f;

    //endless用
    Vector3 moveStartPosition, moveEndPosition;
    bool movePos = false;

    void Start () {
        gimState = GetComponent<GimmickState>();
        startPos = transform.position;

        standingNumber = (decimal)standingNum;
    }

	void Update () {

        if (!moving) {
            if (standingNumber < gimState.gimmickNum) return;
        }
        

        if (endless) MoveObject_Endress();
        else         MoveObject();

	}

    void MoveObject() {
        
        if (moveCount == 0) { 
            moveTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos.position, moveTime);

            if (moveTime >= 1f)
            {
                transform.position = endPos.position;
                moveCount = 1;
                moving = false;
            }
        }
        else
        {
            moveTime += Time.deltaTime;
            transform.position = Vector3.Lerp(endPos.position, startPos, moveTime);

            if (moveTime >= 1f)
            {
                transform.position = startPos;
                moveCount = 0;
                moving = false;
            }
        }
    }

    void MoveObject_Endress() {

        if (!movePos)
        {
            moveStartPosition = startPos;
            moveEndPosition = endPos.position;
        }
        else {
            moveStartPosition = endPos.position;
            moveEndPosition = startPos;
        }

        moveTime += Time.deltaTime;
        transform.position = Vector3.Lerp(moveStartPosition, moveEndPosition, moveTime);

        if (moveTime > 0.1f) moving = true;

        if (moveTime >= 1f) {  
            movePos = !movePos;
            moving = false;
            moveTime = 0f;
        }
    }
}
