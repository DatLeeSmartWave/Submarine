using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopItems_Rocket : MonoBehaviour {
	public int price = 5;
	public int amount = 1;
	public int max = 100;
	public Text priceTxt;
	public Text currentTxt;
	public AudioClip soundPurchased;
	public AudioClip soundFail;

	// Use this for initialization
	void Start () {
		priceTxt.text = price + "";
		currentTxt.text = GlobalValue.Rocket + "/" + max;
	}
	
	public void Buy(){
		if (GlobalValue.Coin >= price && GlobalValue.Rocket < max) {
			SoundManager.PlaySfx (soundPurchased);
			GlobalValue.Coin -= price;
			GPGSManager.Instance.OpenSave(true);
			GlobalValue.Rocket += amount;
			currentTxt.text = GlobalValue.Rocket + "/" + max;
		} else
			SoundManager.PlaySfx (soundFail);
	}
}
