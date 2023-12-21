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
    public TMP_Text roomNameText, playerNameLabel;
    private List<TMP_Text> allPlayerNames = new List<TMP_Text>();

    public GameObject errorScreen;
    public TMP_Text errorText;

    public GameObject roomBrowserScreen;
    public RoomButton buttonRoomBrowser;
    private List<RoomButton> allRoomButtons = new List<RoomButton>();
    private Dictionary<string, RoomInfo> cachedRoomsList = new Dictionary<string, RoomInfo>();

    public GameObject nameInputScreen;
    public TMP_InputField nameInput;
    public static bool hasSetNick;


    public string levelToPlay;
    public GameObject startButton;

    public GameObject roomTestButton;

    // Start is called before the first frame update
    void Start()
    {
        closeMenus();
        loadingScreen.SetActive(true);
        loadingText.text = "Connecting to Network...";
        PhotonNetwork.ConnectUsingSettings();

#if UNITY_EDITOR
        roomTestButton.SetActive(true);
#endif

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void closeMenus()
    {
        loadingScreen.SetActive(false);
        menuButtons.SetActive(false);
        createRoomScreen.SetActive(false); 
        roomScreen.SetActive(false);   
        errorScreen.SetActive(false);
        roomBrowserScreen.SetActive(false);
        nameInputScreen.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
        loadingText.text = "Joining Lobby..";
    }

    public override void OnJoinedLobby()
    {
        closeMenus();
        menuButtons.SetActive(true);

    
        if (!hasSetNick)
        {
            closeMenus();
            nameInputScreen.SetActive(true);

            if (PlayerPrefs.HasKey("playerName"))
            {
                nameInput.text = PlayerPrefs.GetString("playerName");
            }
        }
        else
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("playerName");
        }
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
        ListAllPlayers();

        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
    }

    private void ListAllPlayers()
    {
        foreach(TMP_Text player in allPlayerNames)
        {
            Destroy(player.gameObject);
        }
        allPlayerNames.Clear();

        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;
        for(int i = 0; i < players.Length; i++)
        {
            TMP_Text newPlayerLabel = Instantiate(playerNameLabel, playerNameLabel.transform.parent);
            newPlayerLabel.text = players[i].NickName;
            newPlayerLabel.gameObject.SetActive(true);

            allPlayerNames.Add(newPlayerLabel);

        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        TMP_Text newPlayerLabel = Instantiate(playerNameLabel, playerNameLabel.transform.parent);
        newPlayerLabel.text = newPlayer.NickName;
        newPlayerLabel.gameObject.SetActive(true);

        allPlayerNames.Add(newPlayerLabel);
    }


    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        ListAllPlayers();
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

    public void CloseScreen()
    {   
        closeMenus();
        menuButtons.SetActive(true);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        closeMenus();
        loadingText.text = "Leaving Room...";
        loadingScreen.SetActive(true);
    }

    public override void OnLeftRoom()
    {
        closeMenus();
        menuButtons.SetActive(true);
    }

    public void OpenRoomBrowser()
    {
        closeMenus();
        roomBrowserScreen.SetActive(true);
    }
    public void closeRoomBrowser()
    {
        closeMenus();
        menuButtons.SetActive(true);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateCacheRoomList(roomList);
    }

    public void UpdateCacheRoomList(List<RoomInfo> roomList)
    {
        
        for (int i = 0; i < roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            if (info.RemovedFromList)
            {
                cachedRoomsList.Remove(info.Name);
            }
            else
            {
                cachedRoomsList[info.Name] = info;
            }
        }
        RoomListButtonUpdate(cachedRoomsList);
    }

    void RoomListButtonUpdate(Dictionary<string, RoomInfo> cachedRoomList)
    {
        foreach(RoomButton rb in allRoomButtons)
        {
            Destroy(rb.gameObject);
        }
        allRoomButtons.Clear();
        buttonRoomBrowser.gameObject.SetActive(false);

        foreach (KeyValuePair<string, RoomInfo> roomInfo in cachedRoomList)
        {
            RoomButton newButton = Instantiate(buttonRoomBrowser, buttonRoomBrowser.transform.parent);
            newButton.SetButtonDetails(roomInfo.Value);
            newButton.gameObject.SetActive(true);
            allRoomButtons.Add(newButton);
        }
    }

    public void JoinRoom(RoomInfo inputInfor)
    {
        PhotonNetwork.JoinRoom(inputInfor.Name);
        closeMenus();
        loadingText.text = "Joining Room..";
        loadingScreen.SetActive(true);


    }

    public void SetNickname()
    {
        if (!string.IsNullOrEmpty(nameInput.text))
        {
            PhotonNetwork.NickName = nameInput.text;

            PlayerPrefs.SetString("playerName", nameInput.text);

            closeMenus();
            menuButtons.SetActive(true);

            hasSetNick = true;
        }
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(levelToPlay);

        //PhotonNetwork.LoadLevel(allMaps[Random.Range(0, allMaps.Length)]);
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
    }
    public void QuickJoin()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 8;

        PhotonNetwork.CreateRoom("Test", options);
        closeMenus();
        loadingText.text = "Creating Room...";
        loadingScreen.SetActive(true);
    }

    public void Quit() 
    {
        Application.Quit();
    }

}
