using UnityEngine;
using System.Collections;

//値変更用Enum
enum Enum_GimmickState_TypeD
{
    GRAVITY,    //重力
    HARDNESS,   //硬さ
    SPEED,      //速さ
    ATTACK,     //攻撃力
}

//主に値の設定と変更を行う。Enum_GimmickStateに合わせ見た目の変更もやるかも
public class GimmickStateTypeD : MonoBehaviour {
    public double gimmickNumber = 100; //gimmickNumの数値をInspecterから入力する用
    public decimal gimmickNum;
    public int gimmickNumCount;
    public int digit = 1; //初期の桁数±何桁まで許容するか

    //どの種類のギミックなのかを判断させるbool
    public bool Gravity;
    public bool Door;
    public bool Rotation;
    public bool Light;

    void Start()
    {
        gimmickNum = (decimal)gimmickNumber;
        gimmickNumCount = gimmickNum.ToString().Length + digit;
    }

    void Update()
    {
        gimmickNumber = (double)gimmickNum;
    }
}
