using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour {

	public void ResetAll(){
		PlayerPrefs.DeleteAll ();
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
}
