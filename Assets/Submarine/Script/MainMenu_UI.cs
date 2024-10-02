using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu_UI : MonoBehaviour {
	public Button ShieldBut;
	public Transform ShieldEnegryBar;
	public Text score;
	public Text distance;

	void Awake(){
		ShieldBut.interactable = false;
		ShieldEnegryBar.localScale = new Vector3 (0, 1, 1);
	}


	// Use this for initialization
	void Start () {
		
	}

	void Update(){
		ShieldEnegryBar.localScale = new Vector3 (GameManager.Instance.Player.shieldEnegry / 100, 1, 1);

		ShieldBut.interactable = GameManager.Instance.Player.shieldEnegry == 100;

		score.text = "SCORE: " + GameManager.Instance.Score;
		distance.text = (int)GameManager.Instance.distance + "";
	}


}
