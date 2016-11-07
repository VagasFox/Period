using UnityEngine;
using System.Collections;

public class StopZone : MonoBehaviour {
    Color before_Color;
    Color after_Color;

    [SerializeField]
    private float minusAlpha = 80f;

    private bool colorMove = false;
    private float moveTimer = 0f;

    private bool isMoveDoor = false;
    public bool MoveDoor {
        get { return isMoveDoor; }
        set { isMoveDoor = value; }
    }

    public GameObject[] doors = new GameObject[2];
    private float movePoint = 9f;
    float time = 0f;

    Vector3 firstPosL, firstPosR;
    // Use this for initialization
    void Awake () {
        before_Color = this.GetComponent<Renderer>().material.color;
        after_Color = new Color(before_Color.r, before_Color.g, 
                                before_Color.b,
                                before_Color.a - (minusAlpha / 255f));

        firstPosL = doors[0].transform.position;
        firstPosR = doors[1].transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        AlphaChange();

        if (isMoveDoor) DoorMoveFunction();
    }

    void AlphaChange() {
        Color color = this.GetComponent<Renderer>().material.color;
        moveTimer += Time.deltaTime;

        if (!colorMove)
        {
            color = Color.Lerp(before_Color, after_Color, moveTimer);
        }
        else {
            color = Color.Lerp(after_Color, before_Color, moveTimer);
        }


        if (moveTimer >= 1f)
        {
            colorMove = !colorMove;
            moveTimer = 0f;
        }

        this.GetComponent<Renderer>().material.color = color;
    }

    void DoorMoveFunction()
    {
        Vector3 endPosL = new Vector3(firstPosL.x - movePoint,
                                      firstPosL.y, 
                                      firstPosL.z);

        Vector3 endPosR = new Vector3(firstPosR.x + movePoint,
                                      firstPosR.y,
                                      firstPosR.z);

       

        time += Time.deltaTime;

        doors[0].transform.position = Vector3.Lerp(firstPosL, endPosL, time);

        doors[1].transform.position = Vector3.Lerp(firstPosR, endPosR, time);

        if (time >= 1f) {
            isMoveDoor = false;
            time = 0f;
        }
    }
}
