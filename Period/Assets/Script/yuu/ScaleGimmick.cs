using UnityEngine;
using System.Collections;

public class ScaleGimmick : MonoBehaviour {
    [SerializeField]
    private GameObject GimObj;
    private Vector3 objScale;
    /*[SerializeField]*/
    public bool ScaleX;
    public bool ScaleY;
    public bool ScaleZ;
    private float scale;
    private float xscale = 1;
    private float yscale = 1;
    private float zscale = 1;

    GimmickState gimState;
    // Use this for initialization
    void Start()
    {
        gimState = GetComponent<GimmickState>();
        objScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        scale = (float)gimState.gimmickNum;

        if (ScaleX == true)
        {
            xscale = scale;
        }
        if (ScaleY == true)
        {
            yscale = scale;
        }
        if (ScaleZ == true)
        {
            zscale = scale;
        }

        GimObj.transform.localScale = new Vector3(objScale.x * xscale, objScale.y * yscale, objScale.z * zscale);
    }
}
