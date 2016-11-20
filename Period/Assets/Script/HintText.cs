using UnityEngine;
using System.Collections;
using Enum.Gimmick;

public class HintText : MonoBehaviour {

    /// <summary>
    /// 取得したEnum_GimmickStateからヒント文を設定する
    /// </summary>
    /// <param name="eGim">取得したEnum_GimmickState</param>
    /// <param name="hinttext">適用するstring配列</param>
    /// <param name="obj"アクセスするオブジェクト(DoorGimmick用)</param>
    public static void SetHitnText(GimmickType eGim, ref string[] hinttext, GameObject obj)
    {
        switch (eGim)
        {
            case GimmickType.GRAVITY:
                hinttext[0] = "重力:";
                hinttext[1] = "";
                break;

            case GimmickType.DOOR:
                hinttext[0] = obj.GetComponent<DoorGimmick>().minPass + " < ";
                hinttext[1] = " < " + obj.GetComponent<DoorGimmick>().maxPass;
                break;

            case GimmickType.ROTATE:
                hinttext[0] = "角度:";
                hinttext[1] = "";
                break;

            case GimmickType.LIGHT:
                hinttext[0] = "光度:";
                hinttext[1] = "";
                break;

            case GimmickType.SIZE:
                hinttext[0] = "大きさ";
                hinttext[1] = "";
                break;

            case GimmickType.NEEDLE:
                hinttext[0] = "";
                hinttext[1] = "";
                break;

            case GimmickType.WIND:
                hinttext[0] = "風力";
                hinttext[1] = "";
                break;

            case GimmickType.NONE:
                hinttext[0] = "";
                hinttext[1] = "";
                break;
        }
    }
}
