using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu_GameOver : MonoBehaviour {

	public Text score;
	public Text best;

	// Use this for initialization
	void Start () {
		score.text = GameManager.Instance.Score + "";
		best.text = "Best: " + GlobalValue.Best;
	}
}
