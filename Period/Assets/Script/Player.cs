﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
    private Ray ray;
    private RaycastHit rayHit;
    [SerializeField]
    private GameObject hitObj;

    [SerializeField]
    private GameObject numWindow;                          //数値表示用背景
    private String GimNum;
    private String HintText1;                              //何のギミックなのかを表示させる用
    private String HintText2;                              //何のギミックなのかを表示させる用
    RectTransform rectTransform;                           //numWindowのRectTransform

    [SerializeField]
    private GameObject[] bulletList = new GameObject[2];   //弾丸の種類
    [SerializeField]
    private Transform[] muzzle = new Transform[2];         //弾丸発射点
    [SerializeField]
    private float speed = 1000;                            //弾の発射速度

    private bool g_stateFlag = false;                                       //Enum_Gimmickstateの変更用

    [SerializeField]
    private GameObject rayPoint;
    Vector3 firstPos;
    bool Respawn;
    float feedOut;
    private GameObject BlackOut;

    void Awake()
    {
        rectTransform = numWindow.GetComponent<RectTransform>();
        firstPos = transform.position;
    }

    void Start()
    {
        
        BlackOut = GameObject.Find("BlackOut");
        Respawn = false;
        feedOut = 0;
    }

    void Update()
    {
        ViewLabel();
        ShotBullet();
        PlayerRespawn();

        //一定の高さ以下になった時の復活処理
        if (transform.position.y < -5f)
        {
            GetComponent<RoboMove>().enabled = false;
            Respawn = true;
        }


        if (Input.GetKey(KeyCode.X)) { }
        
    }

    void FixedUpdate()
    {
        PlayerRay();
    }

    /// <summary>
    /// Rayの発射。Gmmickのオブジェクト取得とViewWindowのActive状況の変更
    /// </summary>
    void PlayerRay()
    {
        //Rayの初期化
        ray = new Ray(new Vector3(rayPoint.transform.position.x,
                                  rayPoint.transform.position.y,
                                  rayPoint.transform.position.z), rayPoint.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction, Color.red, 20.0f);
        if (Physics.Raycast(ray, out rayHit, 20.0f))
        {
            //Rayが当たったオブジェクトが「Gimmick」タグが付いているなら
            if (rayHit.collider.CompareTag("Gimmick"))
            {
                //オブジェクトを取得してLabelの表示をさせる
                hitObj = rayHit.collider.gameObject;
                numWindow.SetActive(true);
            }
            else
            {
                //Labelを非表示にしてhitObjを空にする
                numWindow.SetActive(false);
                hitObj = null;
            }
        }
        else
        {
            //hitObjが空なら処理をしない
            if (hitObj == null) return;

            //Labelを非表示にしてhitObjを空にする
            numWindow.SetActive(false);
            hitObj = null;
        }
    }

    /// <summary>
    /// Labelの表示非表示と表示座標更新
    /// </summary>
    void ViewLabel()
    {
        //hitObjが空なら処理をしない
        if (hitObj == null) return;

        //numWindow(Label)の座標の更新
        var worldCamera = Camera.main;
        var targetPos = new Vector3(hitObj.transform.position.x - 1.5f,
                                    hitObj.transform.position.y,
                                    hitObj.transform.position.z);

        var screenPos = RectTransformUtility.WorldToScreenPoint(worldCamera, targetPos);
        rectTransform.localPosition = screenPos;


        //数値が大きくなりすぎないようにするための案①(文字数を制限し、実際の数値をTextに合わせるバージョン)
        GameObject text = numWindow.transform.GetChild(0).gameObject;
        string gimmickNumDisplay = hitObj.GetComponent<GimmickState>().gimmickNum.ToString();
        //文字数制限
        if (gimmickNumDisplay.Length > hitObj.GetComponent<GimmickState>().gimmickNumCount)
        {
            gimmickNumDisplay = gimmickNumDisplay.Remove(hitObj.GetComponent<GimmickState>().gimmickNumCount);
        }
        //ギミックの種類判断と、Textに表示するヒント文
        Enum_GimmickState Gimmick = hitObj.GetComponent<GimmickState>().eGimState;

        SetHintText(Gimmick);

        text.GetComponent<Text>().text = HintText1 + gimmickNumDisplay + HintText2;
        hitObj.GetComponent<GimmickState>().gimmickNum = decimal.Parse(gimmickNumDisplay);
    }

    /// <summary>
    /// 取得したEnum_GimmickStateからヒント文を設定する
    /// </summary>
    /// <param name="eGim">取得したEnum_GimmickState</param>
    void SetHintText(Enum_GimmickState eGim)
    {
        switch (eGim)
        {
            case Enum_GimmickState.GRAVITY:
                HintText1 = "重力:";
                HintText2 = "";
                break;

            case Enum_GimmickState.DOOR:
                HintText1 = hitObj.GetComponent<DoorGimmick>().minPass + " < ";
                HintText2 = " < " + hitObj.GetComponent<DoorGimmick>().maxPass;
                break;

            case Enum_GimmickState.ROTATE:
                HintText1 = "角度:";
                HintText2 = "";
                break;

            case Enum_GimmickState.LIGHT:
                HintText1 = "光度:";
                HintText2 = "";
                break;

            case Enum_GimmickState.SIZE:
                HintText1 = "大きさ";
                HintText2 = "";
                break;

            case Enum_GimmickState.NEEDLE:
                HintText1 = "";
                HintText2 = "";
                break;

            case Enum_GimmickState.WIND:
                HintText1 = "風力";
                HintText2 = "";
                break;

            case Enum_GimmickState.NONE:
                HintText1 = "";
                HintText2 = "";
                break;
        }
    }


    /// <summary>
    /// 弾の発射と弾の種類変更
    /// </summary>
    void ShotBullet()
    {
        //弾の種類変更
        if (Input.GetKeyDown(KeyCode.Z)) {
            if (!SoundManager.isNowSE(SE_Enum.SHOT_CHANGE, this.gameObject)) {
                SoundManager.PlaySE(SE_Enum.SHOT_CHANGE, this.gameObject);
            }
            g_stateFlag = !g_stateFlag;
        }

        if (g_stateFlag)
        {
            muzzle[0].gameObject.SetActive(false);
            muzzle[1].gameObject.SetActive(true);
        }
        else {
            muzzle[0].gameObject.SetActive(true);
            muzzle[1].gameObject.SetActive(false);
        }


        //弾の発射
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameObject bullets = GameObject.Instantiate(bulletList[Convert.ToInt32(g_stateFlag)]) as GameObject;

            //SoundManager.PlaySE(SE_Enum.SHOT_2, muzzle[Convert.ToInt32(g_stateFlag)].gameObject);
            Vector3 force;
            force = this.gameObject.transform.forward * speed;

            bullets.GetComponent<Rigidbody>().AddForce(force);
            bullets.transform.position = muzzle[Convert.ToInt32(g_stateFlag)].position;
        }
    }

    /// <summary>
    /// Playerの復活と暗転明転処理
    /// </summary>
    void PlayerRespawn()
    {
        if (Respawn == true)
        {
            feedOut += Time.deltaTime;
            BlackOut.GetComponent<Image>().color = new Color(0, 0, 0, feedOut);
            if (feedOut >= 1.0f)
            {
                transform.position = firstPos;
                GetComponent<RoboMove>().enabled = true;
                Respawn = false;
            }
        }
        
        if (feedOut > 0.0f && Respawn == false)
        {
            feedOut -= Time.deltaTime * 0.4f;
            BlackOut.GetComponent<Image>().color = new Color(0, 0, 0, feedOut);
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Needle"))
        {
            SoundManager.PlaySE(SE_Enum.NEEDLE, this.gameObject);
            GetComponent<RoboMove>().enabled = false;
            Respawn = true;
        }
    }

}
