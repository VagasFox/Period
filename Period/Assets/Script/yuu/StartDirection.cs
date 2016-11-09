using UnityEngine;
using System.Collections;

public class StartDirection : MonoBehaviour {
    GameObject mPlayer;
    GameObject StartDirectionObj;
    private Vector3 cameraPos;
    private bool St,RotPoint,Rot;
    private float directionTime = 0;
    public float straightMoveNum = 1; //登場時真っすぐ進む距離の大きさ(ここに演出秒数を掛けた分前に進む）
    public float rotationRadius = 1; //グルグル回る時の円の大きさ

    public Animator charaAnim;
    RoboMove RM;
    Player PL;
    // Use this for initialization
    void Start () {
        SoundManager.PlayBGM(BGM_Enum.PLAY_1);
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        RM = mPlayer.GetComponent<RoboMove>();
        RM.enabled = false;

        PL = mPlayer.GetComponent<Player>();
        PL.enabled = false;

        //カメラの最初の位置を記憶
        cameraPos = transform.position;
        //カメラをロボの前方に持ってくる
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.Abs(transform.localPosition.z) * 2);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, 0);
        transform.parent = null;
        //transform.parent = Player.transform;
        St = false;
        Rot = false;
        RotPoint = false;

        StartDirectionObj = new GameObject();
        StartDirectionObj.transform.position = GameObject.Find("StDir").transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (St == false)
        {
            if (directionTime <= 2)
            {
                //runアニメーションを作動させる
                RM.charaAnim.SetFloat("Speed", 1);
                mPlayer.transform.position += mPlayer.transform.forward * straightMoveNum * Time.deltaTime;
                cameraPos += mPlayer.transform.forward * straightMoveNum * Time.deltaTime;
                transform.localPosition += mPlayer.transform.forward * straightMoveNum * Time.deltaTime;
            }
            else
            {
                StartDirectionObj.transform.position = GameObject.Find("StDir").transform.position;
                St = true;
            }
        }

        if (St == true && Rot == false)
        {
            if(RotPoint == false)
            {
                mPlayer.transform.parent = StartDirectionObj.transform;
                RotPoint = true;
            }
            if (directionTime <= 3)
            {
                mPlayer.transform.position += mPlayer.transform.right * rotationRadius * Time.deltaTime;
                StartDirectionObj.transform.eulerAngles -= new Vector3(0, 360 * Time.deltaTime, 0);
            }
            else if (directionTime <= 4)
            {
                mPlayer.transform.position -= mPlayer.transform.right * rotationRadius * Time.deltaTime;
                StartDirectionObj.transform.eulerAngles -= new Vector3(0, 360 * Time.deltaTime, 0);
            }
            else
            {
                Rot = true;
                RM.enabled = true;
                PL.enabled = true;
                transform.parent = mPlayer.transform;
                transform.position = cameraPos;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, 0);
                GetComponent<StartDirection>().enabled = false;
            }
        }

        directionTime += Time.deltaTime;
	}
}
