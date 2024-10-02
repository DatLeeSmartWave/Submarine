using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public enum ITEM_TYPE { iap1, iap2, iap3, iap4, removeAds }

    public ITEM_TYPE itemType;
    public int rewarded = 100;
    //public float price = 100;
    public GameObject watchVideocontainer;

    public AudioClip soundRewarded;

    public Text priceTxt, rewardedTxt, rewardTimeCountDownTxt;

    private void Update()
    {
        UpdateStatus();
    }

    void UpdateStatus()
    {
        if (itemType == ITEM_TYPE.removeAds)
        {
            //priceTxt.text = "$" + price;
            priceTxt.text = Purchaser.Instance.iapRemoveAdText;

            if (GlobalValue.RemoveAds)
                gameObject.SetActive(false);
        }
        else
        {
            //priceTxt.text = "$" + price;
            switch (itemType)
            {
                case ITEM_TYPE.iap1:
                    priceTxt.text = Purchaser.Instance.iapPriceText1;
                    break;
                case ITEM_TYPE.iap2:
                    priceTxt.text = Purchaser.Instance.iapPriceText2;
                    break;
                case ITEM_TYPE.iap3:
                    priceTxt.text = Purchaser.Instance.iapPriceText3;
                    break;
                case ITEM_TYPE.iap4:
                    priceTxt.text = Purchaser.Instance.iapPriceText4;
                    break;
            }

            rewardedTxt.text = "+" + rewarded;
        }
    }

    public void Buy()
    {
        switch (itemType)
        {
            case ITEM_TYPE.iap1:
                Purchaser.iAPResult += Purchaser_iAPResult;
                Purchaser.Instance.BuyItem1();
                break;
            case ITEM_TYPE.iap2:
                Purchaser.iAPResult += Purchaser_iAPResult;
                Purchaser.Instance.BuyItem2();
                break;
            case ITEM_TYPE.iap3:
                Purchaser.iAPResult += Purchaser_iAPResult;
                Purchaser.Instance.BuyItem3();
                break;
            case ITEM_TYPE.iap4:
                Purchaser.iAPResult += Purchaser_iAPResult;
                Purchaser.Instance.BuyItem4();
                break;
            case ITEM_TYPE.removeAds:
                Purchaser.iAPResult += Purchaser_iAPResult;
                Purchaser.Instance.BuyRemoveAds();
                break;
        }
    }

    private void Purchaser_iAPResult(int id)
    {
        if (itemType == ITEM_TYPE.removeAds)
        {
            GlobalValue.RemoveAds = true;
        }
        else
        {
            Purchaser.iAPResult -= Purchaser_iAPResult;
            GlobalValue.Coin += rewarded;
            GPGSManager.Instance.OpenSave(true);
            SoundManager.PlaySfx(soundRewarded);
            UpdateStatus();
        }
    }
}