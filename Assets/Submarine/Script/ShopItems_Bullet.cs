using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopItems_Bullet : MonoBehaviour {
	public int price = 10;
	public int amount = 10;
	public int max = 100;
	public Text priceTxt;
	public Text currentTxt;
	public AudioClip soundPurchased;
	public AudioClip soundFail;

	// Use this for initialization
	void Start () {
		priceTxt.text = price + "";
		currentTxt.text = GlobalValue.Bullet + "/" + max;
	}
	
	public void Buy(){
		if (GlobalValue.Coin >= price && GlobalValue.Bullet < max) {
			SoundManager.PlaySfx (soundPurchased);
			GlobalValue.Coin -= price;
			GPGSManager.Instance.OpenSave(true);
			GlobalValue.Bullet += amount;
			currentTxt.text = GlobalValue.Bullet + "/" + max;
		} else
			SoundManager.PlaySfx (soundFail);
	}
}
