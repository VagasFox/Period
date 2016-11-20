using UnityEngine;
using System.Collections;
using Enum.Gimmick;


//主に値の設定と変更を行う。Enum_GimmickStateに合わせ見た目の変更もやるかも
public class GimmickState : MonoBehaviour {

    public double gimmickNumber = 100; //gimmickNumの数値をInspecterから入力する用
    public decimal gimmickNum;
    public int gimmickNumCount;
    public int digit = 1; //初期の桁数±何桁まで許容するか

    public GimmickType eGimState;
  

    void Start()
    {
        gimmickNum = (decimal)gimmickNumber;
        gimmickNumCount = gimmickNum.ToString().Length + digit;

        if (gimmickNumCount <= 2)
        {
            gimmickNumCount = 3;
        }
    }

    void Update()
    {
        gimmickNumber = (double)gimmickNum;
    }
}