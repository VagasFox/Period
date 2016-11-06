using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoalSctipt : MonoBehaviour {
    //ステージセレクトに行く前の演出時間設定
    public float directionTime;
    private bool Goal;

    void Start()
    {
        Goal = false;
    }

    void Update()
    {
        if (Goal == true)
        {
            directionTime -= Time.deltaTime;

            if (directionTime <= 0) SceneManager.LoadScene("StageSelect");
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
