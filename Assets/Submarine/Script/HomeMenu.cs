using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HomeMenu : MonoBehaviour {
	public float gotoPlayIn = 1;
	float time;
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > gotoPlayIn) {
			GotoPlay ();
			enabled = false;
		}
	}

	public void GotoPlay(){
		SceneManager.LoadSceneAsync ("Play");
	}
}
