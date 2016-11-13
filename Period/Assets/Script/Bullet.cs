using UnityEngine;
using System.Collections;
using Enum.Sound;

public enum Enum_BulletType {
    Multi = 0,  //掛け算
    Div,        //割り算
    Attack,     //攻撃
}

public class Bullet : MonoBehaviour
{
    public Enum_BulletType eBulletType;
    private int gimmickNumberCount = 100;

    void Start()
    {
        SoundManager.PlaySE(SE_Enum.SHOT_1, this.gameObject);
    }

    void Update()
    {

    }

    /// <summary>
    /// 掛け算弾
    /// </summary>
    /// <param name="target_Num">被弾した「Gimmick」オブジェクトの設定数値</param>
    /// <returns></returns>
    decimal BulletMultiplication(decimal target_Num)
    {
        //数値が10以上で、現在の桁数が初期の桁数より大きい場合は乗算しない
        if (target_Num / 10 >= 1 && target_Num.ToString().Length - 1 >= gimmickNumberCount)
        {
            return target_Num;
        }
        return target_Num * 10;
    }

    /// <summary>
    /// 割り算弾
    /// </summary>
    /// <param name="target_Num">被弾した「Gimmick」オブジェクトの設定数値</param>
    /// <returns></returns>
    decimal BulletDivision(decimal target_Num)
    {
        //小数点1桁以下にならないようにする
        if (target_Num / 10 >= 0.1m)
        {
            return target_Num * 0.1m;
        }
        return target_Num;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Gimmick"))
        {
            GimmickState colState = col.collider.GetComponent<GimmickState>();

            //対象ギミックの桁数制限を取得
            gimmickNumberCount = colState.gimmickNumCount;

            if (eBulletType == Enum_BulletType.Multi)
            {
                colState.gimmickNum = BulletMultiplication(colState.gimmickNum);
            }

            else if (eBulletType == Enum_BulletType.Div)
            {
                colState.gimmickNum = BulletDivision(colState.gimmickNum);
            }

            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name == "BulletGard") {
            Destroy(this.gameObject);
        }
    }
}
