using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject[] submarines;

    private void OnEnable()
    {
        foreach (var obj in submarines)
        {
            obj.SetActive(GameManager.Instance.State != GameManager.GameState.Pause);
        }
    }
}