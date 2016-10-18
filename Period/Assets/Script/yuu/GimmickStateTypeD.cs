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
    public double gimmickNumber = 100;
    public decimal gimmickNum;
    public decimal beforeGimmickNum;
    public int gimmickNumCount;
    public int digit = 1;

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
