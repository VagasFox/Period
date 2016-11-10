using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour {
	float timeDelay = 0;
	bool SS = false;
	string SceneName;
    public void SceneLoad(string SN)
    {
		SS = true;
		SceneName = SN;
    }

	void Update(){
		if (SS == true) {
			timeDelay += Time.deltaTime;
			if(timeDelay >= 1.3f) SceneManager.LoadScene(SceneName);
		}

	}
}
