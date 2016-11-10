using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoalSctipt : MonoBehaviour {
    //ステージセレクトに行く前の演出時間設定
    public string NextStageName;
    public float directionTime;
    private bool Goal;
    private GameObject BlackOut;
    float feedOut;

    void Start()
    {
        Goal = false;
        BlackOut = GameObject.Find("BlackOut");
    }

    void Update()
    {
        if (Goal == true)
        {
            directionTime -= Time.deltaTime;
            feedOut += Time.deltaTime / (directionTime);
            BlackOut.GetComponent<Image>().color = new Color(0, 0, 0, feedOut);

            if (directionTime <= 0) SceneManager.LoadScene(NextStageName);
        }
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.tag == "Player")
        {
            Goal = true;
            player.GetComponent<RoboMove>().enabled = false;
        }
    }
}
