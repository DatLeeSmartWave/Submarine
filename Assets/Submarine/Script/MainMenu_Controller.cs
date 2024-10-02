using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu_Controller : MonoBehaviour {
	bool isMoveUp = false;
//	public GameObject Rocket;
	public GameObject Shield;

	public Text bullet;
	public Text rocket;

	bool isFire = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(isMoveUp)
			GameManager.Instance.Player.MoveUp ();

		if(isFire)
			GameManager.Instance.Player.FireBullet ();

//		Rocket.SetActive (GlobalValue.Rocket > 0);
		Shield.SetActive (GameManager.Instance.Player.shieldEnegry == 100);


		rocket.text = GlobalValue.Rocket + "";
		bullet.text = GlobalValue.Bullet + "";
	}

	public void UseShield(){
		GameManager.Instance.Player.UseShield ();
	}

	public void FireRocket(){
			GameManager.Instance.Player.Fire ();
	}

	public void MoveUp(){
		isMoveUp = true;
	}

	public void MoveUpOff(){
		isMoveUp = false;
	}

	public void FireGun(){
		isFire = true;
	}
	public void FireGunOff(){
		isFire = false;
	}
}
