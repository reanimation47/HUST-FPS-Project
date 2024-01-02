using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class UIdeath : MonoBehaviour
{
    public static UIdeath instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject deathScreen;
    public TMP_Text deathText;

    public GameObject leaderboard;
    public LeaderBoard leaderboardPlayerDisplay;

    public GameObject optionsScreen;

    public GameObject endScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowHideOptions();
        }

       /* if (optionsScreen.activeInHierarchy && Cursor.lockState != CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }*/
    }
    public void ShowHideOptions()
    {
        if (!optionsScreen.activeInHierarchy)
        {
            optionsScreen.SetActive(true);
        }
        else
        {
            optionsScreen.SetActive(false);
        }
    }

    public void ReturnToMainMenu()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.LeaveRoom();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
