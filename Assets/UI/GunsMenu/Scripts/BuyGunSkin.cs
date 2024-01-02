using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuyGunSkin
{
    public void BuySkin(int skinPrice)
    {
        int currentCoin = PlayerPrefs.GetInt("CoinOwned");
        PlayerPrefs.SetInt("CoinOwned", currentCoin - skinPrice);
    }
}
