using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;
    //delegate   ()
    public delegate void RewardedAdResult(bool isSuccess, int rewarded);

    //event  
    public static event RewardedAdResult AdResult;

    [Header("REWARDED VIDEO AD")]
    public int getRewarded = 5;
    public float timePerWatch = 90;
    float lastTimeWatch = -999;

    [Header("SHOW AD VICTORY/GAMEOVER")]
    public int showAdGameOverCounter = 2;
    int counter_gameOver = 0;

    private void Awake()
    {
        if (AdsManager.Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ShowAdmobBanner(bool show)
    {
        if (GlobalValue.RemoveAds)
        {
            Debug.LogWarning("Ads Remove");
            return;
        }

        AdmobController.Instance.ShowBanner(show);
    }

    #region NORMAL AD

    public void ShowNormalAd()
    {
        if (GlobalValue.RemoveAds)
        {
            Debug.LogWarning("Ads Remove");
            return;
        }

        StartCoroutine(ShowNormalAdCo(0));
    }

    IEnumerator ShowNormalAdCo(float delay)
    {
        yield return new WaitForSeconds(delay);
        counter_gameOver++;
        if (counter_gameOver >= showAdGameOverCounter)
        {
            if (AdmobController.Instance.ForceShowInterstitialAd())
            {
                counter_gameOver = 0;
            }
        }
    }

    public void ResetCounter()
    {
        counter_gameOver = 0;
    }

    #endregion

    #region REWARDED VIDEO AD

    public bool isRewardedAdReady()
    {
        if (AdmobController.Instance.isRewardedVideoAdReady())
            return true;

        return false;

    }

    public float TimeWaitingNextWatch()
    {
        return timePerWatch - (Time.realtimeSinceStartup - lastTimeWatch);
    }

    public void ShowRewardedAds()
    {
        lastTimeWatch = Time.realtimeSinceStartup;

        AdmobController.AdResult += AdmobController_AdResult;
        AdmobController.Instance.WatchRewardedVideoAd();
    }

    private void AdmobController_AdResult(bool isWatched)
    {
        AdmobController.AdResult -= AdmobController_AdResult;
        AdResult(true, getRewarded);
    }
    #endregion
}