using UnityEngine;
using System.Collections;

//値変更用Enum
enum Enum_GimmickState
{
    GRAVITY,    //重力
    HARDNESS,   //硬さ
    SPEED,      //速さ
    ATTACK,     //攻撃力
}

//主に値の設定と変更を行う。Enum_GimmickStateに合わせ見た目の変更もやるかも
public class GimmickState : MonoBehaviour {

    public float gimmickNum = 55.5555f;
    public int gimmickNumCount;

    void Start() {
        gimmickNumCount = gimmickNum.ToString().Length;
    }

    void Update()
    {
    }
}
