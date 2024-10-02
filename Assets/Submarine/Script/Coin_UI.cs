using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Coin_UI : MonoBehaviour {
	public Text coin;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		coin.text = GlobalValue.Coin + "";
	}
}
