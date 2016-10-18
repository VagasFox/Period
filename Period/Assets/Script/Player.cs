using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Player : MonoBehaviour {
    private Ray ray;
    private RaycastHit rayHit;
    private GameObject hitObj;

    [SerializeField] private GameObject numWindow;                          //数値表示用背景
    RectTransform rectTransform;                                            //numWindowのRectTransform

    [SerializeField] private GameObject[] bulletList = new GameObject[2];   //弾丸の種類
    [SerializeField] private Transform muzzle;                              //弾丸発射点
    [SerializeField] private float speed = 1000;                            //弾の発射速度

    private bool g_stateFlag = false;                                       //Enum_Gimmickstateの変更用

    void Awake() {
        rectTransform = numWindow.GetComponent<RectTransform>();
    }

    void Update()
    {
        ViewLabel();
        PlayerRay();
        ShotBullet();
    }

    /// <summary>
    /// Rayの発射。Gmmickのオブジェクト取得とViewWindowのActive状況の変更
    /// </summary>
    void PlayerRay() {
        //Rayの初期化
        ray = new Ray(new Vector3(transform.position.x,
                                  transform.position.y + 1f,
                                  transform.position.z), transform.forward);

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
            else {
                //Labelを非表示にしてhitObjを空にする
                numWindow.SetActive(false);
                hitObj = null;
            }
        }
        else {
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
        var targetPos = new Vector3(hitObj.transform.position.x, 
                                    hitObj.transform.position.y,
                                    hitObj.transform.position.z);

        var screenPos = RectTransformUtility.WorldToScreenPoint(worldCamera, targetPos);
        rectTransform.localPosition = screenPos;

        //Textの表示の更新
        //GameObject text = numWindow.transform.GetChild(0).gameObject;
        //text.GetComponent<Text>().text = hitObj.GetComponent<GimmickState>().gimmickNum.ToString();

        //数値が大きくなりすぎないようにするための案①(Text表示を制限し、実際の数値をTextに合わせるバージョン)
        GameObject text = numWindow.transform.GetChild(0).gameObject;
        text.GetComponent<Text>().text = hitObj.GetComponent<GimmickState>().gimmickNum.ToString();
        hitObj.GetComponent<GimmickStateTypeD>().gimmickNum = decimal.Parse(text.GetComponent<Text>().text);
    }

    /// <summary>
    /// 弾の発射と弾の種類変更
    /// </summary>
    void ShotBullet() {
        //弾の種類変更
        if (Input.GetKeyDown(KeyCode.Z)) g_stateFlag = !g_stateFlag;
            
        //弾の発射
        if (Input.GetKeyDown(KeyCode.Return)) {
            GameObject bullets = GameObject.Instantiate(bulletList[Convert.ToInt32(g_stateFlag)]) as GameObject;

            Vector3 force;
            force = this.gameObject.transform.forward * speed;

            bullets.GetComponent<Rigidbody>().AddForce(force);
            bullets.transform.position = muzzle.position;
        }
    }

}
