using UnityEngine;
using System.Collections;

public class RotationGimmick : MonoBehaviour
{
    [SerializeField]
    private GameObject Bridge;
    /*[SerializeField]*/
    private float rotation;


    float r_time;
    public float timeSpped = 3f;

    GimmickState gimState;
    // Use this for initialization
    void Start()
    {
        gimState = gameObject.GetComponent<GimmickState>();
    }

    // Update is called once per frame
    void Update()
    {
        rotation = (float)gimState.gimmickNum;
        Bridge.transform.rotation = Quaternion.Euler(0, rotation, 0);
    }
}
