using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    public TMP_Text playerNameText, killsText;

    public static LeaderBoard instance;
    private void Awake()
    {
        instance = this;
    }
    public void SetDetails(string name, string KD)
    {
        playerNameText.text = name;
        killsText.text = KD;
    }
}
