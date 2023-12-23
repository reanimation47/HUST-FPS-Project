using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class UserController : MonoBehaviour
{
    public UserDatabase userDB;

    [Header("User Infor")]
    public TextMeshProUGUI coinOwned;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUserOwned();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUserOwned();
    }

    private void UpdateUserOwned()
    {
        UserAttribute user = userDB.UserOwned;
        int currentCoin = PlayerPrefs.GetInt("CoinOwned");
        if (currentCoin > 0)
        {
            coinOwned.text = currentCoin.ToString();
        }
        else
        {
            coinOwned.text = user.coinOwned.ToString();
        }

    }

    public void UpdateCoinAfterBuying()
    {
        int currentCoin = PlayerPrefs.GetInt("CoinOwned");
        PlayerPrefs.SetInt("CoinOwned", currentCoin - 2500);

    }

    public void EarnCoin ()
    {
        int currentCoin = PlayerPrefs.GetInt("CoinOwned");
        PlayerPrefs.SetInt("CoinOwned", currentCoin + 2500);
    }
}
