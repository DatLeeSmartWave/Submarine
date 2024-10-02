using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

public class UnityAds : MonoBehaviour {

	public AudioClip soundReward;
	public Text rewardTxt;
	public int reward = 10;

	// Use this for initialization
	void Start () {
		rewardTxt.text = reward + "";
	}



	public void ShowRewardVideo(){
		#if UNITY_ADS
		ShowRewardedAd ();
		#else
		Debug.Log("You need to turn on the Ads in Services tab to able watch the video ads to earn stars");
		#endif
	}

	public void ShowNormalAd()
	{	
		#if UNITY_ADS
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}
		#else
		Debug.Log("You need to turn on the Ads in Services tab to able watch the video ads to earn stars");
		#endif
	}

	#if UNITY_ADS

	private void ShowRewardedAd()
	{

	if (Advertisement.IsReady("rewardedVideo"))
	{
	var options = new ShowOptions { resultCallback = HandleShowResult };
	Advertisement.Show("rewardedVideo", options);
	}
	}

	private void HandleShowResult(ShowResult result)
	{
	switch (result)
	{
	case ShowResult.Finished:
	Debug.Log ("The ad was successfully shown.");

	GlobalValue.Coin += reward;
	SoundManager.PlaySfx (soundReward);

	break;
	case ShowResult.Skipped:
	Debug.Log("The ad was skipped before reaching the end.");
	break;
	case ShowResult.Failed:
	Debug.LogError("The ad failed to be shown.");
	break;
	}
	}
	#endif
}

