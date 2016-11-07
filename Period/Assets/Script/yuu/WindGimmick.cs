using UnityEngine;
using System.Collections;

public class WindGimmick : MonoBehaviour {
    public bool XQuarter;
    public bool YQuarter;
    public bool ZQuarter;
    float windPower;
    GimmickState gimState;
    WindPower WP;
    // Use this for initialization
    void Start()
    {
        gimState = GetComponent<GimmickState>();
        WP = GameObject.Find("Wind").GetComponent<WindPower>();
    }
	
	// Update is called once per frame
	void Update () {
        windPower = (float)gimState.gimmickNum;

        if (XQuarter == true)
        {
            WP.xWindPower = windPower;
        }
        if (YQuarter == true)
        {
            WP.yWindPower = windPower;
        }
        if (ZQuarter == true)
        {
            WP.zWindPower = windPower;
        }
	}

    
}
