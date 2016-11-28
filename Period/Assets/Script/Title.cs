using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Enum.Sound;

public class Title : MonoBehaviour {
    [SerializeField]
    private GameObject[] biribiri = new GameObject[2];

	public float switchTime = 0f;
	public bool flag = false;

    [SerializeField]
    private float plusScale = 1.5f;

    SceneSwitch s_switch;

    void Awake()
    {
        SoundManager.PlayBGM(BGM_Enum.TITLE);
        s_switch = GetComponent<SceneSwitch>();
        UnityEngine.Cursor.visible = true;
    }

    void Update () {
        SwitchLogo();
        if (Input.GetKeyDown(KeyCode.Return)) s_switch.SceneLoad("StageSelect");
    }

    /// <summary>
    /// タイトルロゴの電気マークの点滅演出
    /// </summary>
    void SwitchLogo() {
        switchTime += Time.deltaTime;

        if (switchTime > 0.5f)
        {
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
