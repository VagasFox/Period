using UnityEngine;
using System.Collections;

public enum Enum_BulletType_TypeD
{
    MultiTypeD = 0,  //掛け算
    DivTypeD         //割り算
}

public class BulletTypeD : MonoBehaviour {
    public Enum_BulletType_TypeD eBulletType;
    public int gimmickNumberCount = 100;

    void Start()
    {
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
        if(target_Num / 10 >= 0.1m)
        {
            return target_Num * 0.1m;
        }
            return target_Num;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Gimmick"))
        {
            GimmickStateTypeD colState = col.collider.GetComponent<GimmickStateTypeD>();
            
            //対象ギミックの桁数制限を取得
            gimmickNumberCount = colState.gimmickNumCount;

            if (eBulletType == Enum_BulletType_TypeD.MultiTypeD)
            {
                colState.gimmickNum = BulletMultiplication(colState.gimmickNum);
            }

            else if (eBulletType == Enum_BulletType_TypeD.DivTypeD)
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
}
