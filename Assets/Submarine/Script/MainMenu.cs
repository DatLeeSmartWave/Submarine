﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	public static MainMenu Instance;

	public GameObject UI;
	public GameObject Controller;
	public GameObject Gameover;
	public GameObject Gamepause;
	public GameObject Shop;
	public GameObject Mission;
	public GameObject Loading;
	public GameObject Coin;
	public GameObject PlayBtn;

	[Header("Settings")]
	public GameObject Settings;
	public Image sound;
	public Sprite soundOn;
	public Sprite soundOff;
	public Image music;
	public Sprite musicOn;
	public Sprite musicOff;

	void Awake(){
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		UI.SetActive (false);
		Controller.SetActive (false);
		Gameover.SetActive (false);
		Gamepause.SetActive (false);
		Shop.SetActive (false);
		Settings.SetActive (false);
		Mission.SetActive (false);
		Loading.SetActive (false);
		Coin.SetActive (false);

		sound.sprite = GlobalValue.isSound ? soundOn : soundOff;
		music.sprite = GlobalValue.isMusic ? musicOn : musicOff;

		SoundManager.SoundVolume = GlobalValue.isSound ? SoundManager.SoundVolume : 0;
		SoundManager.MusicVolume = GlobalValue.isMusic ? SoundManager.MusicVolume : 0;
	}

	public void Play(){
		PlayBtn.SetActive(false);
		UI.SetActive(true);
		Controller.SetActive(true);
		Coin.SetActive(true);
		GameManager.Instance.Play ();
	}

	//called by GameManager
	public void GameOver(){
		UI.SetActive (false);

		StartCoroutine (ShowPanelCo (Mission, 1.5f));
		StartCoroutine (ShowPanelCo (Gameover, 2));
	}

	public void Restart(){
		SoundManager.PlaySfx (GameManager.Instance.SoundManager.soundClick);
		if (MissionManager.Instance.isAnyTaskCompleted ())
			MissionManager.Instance.GetAllRewarded ();

		Loading.SetActive (true);
		SceneManager.LoadSceneAsync (SceneManager.GetActiveScene ().buildIndex);
	}

	public void Home(){
		SoundManager.PlaySfx (GameManager.Instance.SoundManager.soundClick);
		SceneManager.LoadSceneAsync ("HomeMenu");
	}

	public void OpenShop(){
		SoundManager.PlaySfx (GameManager.Instance.SoundManager.soundClick);
		Shop.SetActive (true);
	}

	public void OpenSettings(){
		SoundManager.PlaySfx (GameManager.Instance.SoundManager.soundClick);
		Settings.SetActive (true);
	}

	public void Exit(){
		Application.Quit ();
	}

	public void Pause(){
		if (Time.timeScale == 1) {
			Time.timeScale = 0;
			GameManager.Instance.State = GameManager.GameState.Pause;
			Gamepause.SetActive(true);
		} else {
			Time.timeScale = 1;
			Gamepause.SetActive (false);
			GameManager.Instance.State = GameManager.GameState.Playing;
		}
		SoundManager.PlaySfx (GameManager.Instance.SoundManager.soundClick);
	}

	IEnumerator ShowPanelCo(GameObject panel,float time){
		yield return new WaitForSeconds (time);
		panel.SetActive (true);
	} 

	/// <summary>
	/// Settings
	/// </summary>
	/// 
	public void TurnSound(){
		GlobalValue.isSound = !GlobalValue.isSound;
		sound.sprite = GlobalValue.isSound ? soundOn : soundOff;

		SoundManager.SoundVolume = GlobalValue.isSound ? 1 : 0;

		SoundManager.PlaySfx (GameManager.Instance.SoundManager.soundClick);
	}

	public void TurnMusic(){
		GlobalValue.isMusic = !GlobalValue.isMusic;
		music.sprite = GlobalValue.isMusic ? musicOn : musicOff;

		SoundManager.MusicVolume = GlobalValue.isMusic ? GameManager.Instance.SoundManager.musicsGameVolume : 0;

		SoundManager.PlaySfx (GameManager.Instance.SoundManager.soundClick);
	}

	void OnDisable(){
		Time.timeScale = 1;
	}
}
