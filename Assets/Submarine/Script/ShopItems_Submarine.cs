using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopItems_Submarine : MonoBehaviour {
	public int price;
	public GameObject CharacterPrefab;

	public bool unlockDefault = false;

	//	public GameObject Locked;
	public GameObject UnlockButton;
	public GameObject StateButton;

	public Text pricetxt;
	public Text state; 

	public AudioClip soundPurchased;
	public AudioClip soundFail;

	bool isUnlock;
	SoundManager soundManager;
	int CharacterID;

	// Use this for initialization
	void Start () {
		CharacterID = CharacterPrefab.GetInstanceID ();
		soundManager = FindObjectOfType<SoundManager> ();

		if (unlockDefault)
			isUnlock = true;
		else
			isUnlock = GlobalValue.isCharacterUnlocked (CharacterID);

		//		Locked.SetActive (!isUnlock);
		UnlockButton.SetActive (!isUnlock);
		StateButton.SetActive (isUnlock);

		pricetxt.text = price.ToString ();
	}

	void Update(){

		if (!isUnlock)
			return;

		if (GlobalValue.CharacterPicked (0, false) == CharacterID || (GlobalValue.CharacterPicked (0, false) == 0 && unlockDefault))
			state.text = "Picked";
		else
			state.text = "Choose";
	}

	public void Unlock(){
		SoundManager.PlaySfx (soundManager.soundClick);

		if (GlobalValue.Coin >= price) {
			GlobalValue.Coin -= price;
			GPGSManager.Instance.OpenSave(true);
			//Unlock
			GlobalValue.UnlockCharacter (CharacterID);
			isUnlock = true;
			//			Locked.SetActive (false);
			UnlockButton.SetActive (!isUnlock);
			StateButton.SetActive (isUnlock);

			SoundManager.PlaySfx (soundPurchased);
		} else
			SoundManager.PlaySfx (soundFail);
	}

	public void Pick(){
		SoundManager.PlaySfx (soundManager.soundClick);
		GlobalValue.CharacterPicked (CharacterID,true);
	}
}
