using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject loadingScreen;
    public TMP_Text loadingText;
    public GameObject menuButtons;
    public GameObject createRoomScreen;
    public TMP_InputField roomNameInput;
    public GameObject roomScreen;
    public TMP_Text roomNameText;
    public GameObject errorScreen;
    public TMP_Text errorText;

    // Start is called before the first frame update
    void Start()
    {
        closeMenus();
        loadingScreen.SetActive(true);
        loadingText.text = "Connecting to Network...";
        PhotonNetwork.ConnectUsingSettings(); 
    }

    void closeMenus()
    {
        loadingScreen.SetActive(false);
        menuButtons.SetActive(false);
        createRoomScreen.SetActive(false); 
        roomScreen.SetActive(false);   
        errorScreen.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        
        PhotonNetwork.JoinLobby();

        loadingText.text = "Joining Lobby..";
    }

    public override void OnJoinedLobby()
    {
        closeMenus();
        menuButtons.SetActive(true);
    }

    // Update is called once per frame
    public  void OpenRoomCreated()
    {
        closeMenus();
        createRoomScreen.SetActive(true);
    }

    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(roomNameInput.text))
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 8;
            PhotonNetwork.CreateRoom(roomNameInput.text, options);
            closeMenus();
            loadingText.text = "Creating Room..";
            loadingScreen.SetActive(true);
        }
    }
    public override void OnJoinedRoom()
    {
        closeMenus();
        roomScreen.SetActive(true);

        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Fail to Create Room: "+ message;
        closeMenus();
        errorScreen.SetActive(true);
    }

    public void CloseErrorScreen()
    {
        closeMenus();
        menuButtons.SetActive(true);
    }
}
