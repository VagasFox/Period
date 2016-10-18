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

    public decimal gimmickNum = 55.5055m;
    public decimal beforeGimmickNum;
    public int gimmickNumCount;

    void Start()
    {
        gimmickNumCount = gimmickNum.ToString().Length;
    }

    void Update()
    {
    }
}
