using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
    [SerializeField]
    private GameObject[] biribiri = new GameObject[2];

    private float switchTime = 0f;
    private bool flag = false;

    [SerializeField]
    private float plusScale = 1.5f;

    void Awake()
    {
        SoundManager.PlayBGM(BGM_Enum.TITLE);
    }

    void Update () {
        switchTime = Time.deltaTime;

        if (switchTime > 0.5f) {
            flag = !flag;
            switchTime = 0f;
        }

        if (flag)
        {
            biribiri[0].transform.localScale = new Vector3(plusScale, plusScale, plusScale);
            biribiri[1].transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else {
            biribiri[0].transform.localScale = new Vector3(1f, 1f, 1f);
            biribiri[1].transform.localScale = new Vector3(plusScale, plusScale, plusScale);
        }

    }
}
