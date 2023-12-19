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

    public GameObject roomBrowserScreen;
    public RoomButton buttonRoomBrowser;
    private List<RoomButton> allRoomButtons = new List<RoomButton>();
    private Dictionary<string, RoomInfo> cachedRoomsList = new Dictionary<string, RoomInfo>();

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
        roomBrowserScreen.SetActive(false);
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

}
