using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour {

    public void SceneLoad(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
