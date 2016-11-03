using UnityEngine;
using System.Collections;

public class NeedleGimmick : MonoBehaviour {
    [SerializeField] private GameObject[] RotateGim = new GameObject[4];
    private float rotation;

    GimmickState gimState;
	void Start () {
        gimState = GetComponent<GimmickState>();
	}

    void Update()
    {
        rotation = (float)gimState.gimmickNum;
        RotateGim[0].transform.rotation = Quaternion.Euler(rotation, 0, 0);
        RotateGim[1].transform.rotation = Quaternion.Euler(180f-rotation, 0, 0);
        RotateGim[2].transform.rotation = Quaternion.Euler(rotation, 0, 0);
        RotateGim[3].transform.rotation = Quaternion.Euler(180f-rotation, 0, 0);
    }
}
