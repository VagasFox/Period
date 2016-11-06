using UnityEngine;
using System.Collections;

public class GimmickManager : MonoBehaviour {
   

    [System.Serializable]
    public class Gimmicks {
        public GameObject gimmickObjs;
        public float gimmickPoint;  //指定する数値
    }

    public Gimmicks[] gimmickList;
    void Awake () {
        
        for (int i = 0; i < gimmickList.Length; i++) {
            gimmickList[i].gimmickObjs.GetComponent<GimmickState>().gimmickNum = (decimal)gimmickList[i].gimmickPoint;
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
