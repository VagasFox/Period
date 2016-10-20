using UnityEngine;
using System.Collections;

//値変更用Enum
public enum Enum_GimmickState
{
    GRAVITY,    //重力
    HARDNESS,   //硬さ
    SPEED,      //速さ
    ATTACK,     //攻撃力
    DOOR,       //ドア
    LIGHT,      //ライト
    ROTATE,     //回転
    NONE,       //何も無し
}

//主に値の設定と変更を行う。Enum_GimmickStateに合わせ見た目の変更もやるかも
public class GimmickState : MonoBehaviour {

    public double gimmickNumber = 100; //gimmickNumの数値をInspecterから入力する用
    public decimal gimmickNum;
    public int gimmickNumCount;
    public int digit = 1; //初期の桁数±何桁まで許容するか

    public Enum_GimmickState eGimState;
  

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