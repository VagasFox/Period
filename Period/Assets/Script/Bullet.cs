using UnityEngine;
using System.Collections;

public enum Enum_BulletType {
    Multi = 0,  //掛け算
    Div         //割り算
}

public class Bullet : MonoBehaviour {
    public Enum_BulletType eBulletType;

	void Start () {
    }
	
	void Update () {
        
    }

    /// <summary>
    /// 掛け算弾
    /// </summary>
    /// <param name="target_Num">被弾した「Gimmick」オブジェクトの設定数値</param>
    /// <returns></returns>
    float BulletMultiplication(float target_Num)
    {
        if (target_Num % 10 != 0)
        {
            return target_Num * 10;
        }
        return target_Num;
    }

    /// <summary>
    /// 割り算弾
    /// </summary>
    /// <param name="target_Num">被弾した「Gimmick」オブジェクトの設定数値</param>
    /// <returns></returns>
    float BulletDivision(float target_Num)
    {
        //小数点1桁以下にならないようにする
        if (target_Num / 10 >= 0.1f)
        {
            return target_Num * 0.1f;
        }
        return target_Num;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Gimmick"))
        {
            GimmickState colState = col.collider.GetComponent<GimmickState>();

            if (eBulletType == Enum_BulletType.Multi)
            {
                colState.gimmickNum = BulletMultiplication(colState.gimmickNum);
            }

            else if (eBulletType == Enum_BulletType.Div)
            {
                colState.gimmickNum = BulletDivision(colState.gimmickNum);
            }
                
            Destroy(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }
}
