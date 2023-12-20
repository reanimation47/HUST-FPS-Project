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
        UpdateGun();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateGun()
    {
        UserAttribute user = userDB.UserOwned;

        coinOwned.text = user.coinOwned.ToString();
    }
}
