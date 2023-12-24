using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.InputSystem.XR;

public class MatchManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public static MatchManager instance;
    public float TargetKills = 10;

    private void Awake()
    {
        instance = this;
    }

    public List<PlayerInfo> allPlayers = new List<PlayerInfo>();
    private int index;
    public GameState state = GameState.Waiting;

    private List<LeaderBoard> lboardPlayers = new List<LeaderBoard>();
    public enum EventCodes : byte
    {
        NewPlayer,
        ListPlayers,
        UpdateStat,
        NextMatch,
        TimerSync
    }

    public enum GameState
    {
        Waiting,
        Playing,
        Ending
    }


    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene(0);
        } else
        {
            NewPlayerSend(PhotonNetwork.NickName);

            state = GameState.Playing;

           // SetupTimer();

            if (!PhotonNetwork.IsMasterClient)
            {
                //UIController.instance.timerText.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && state != GameState.Ending)
        {
            if (UIdeath.instance.leaderboard.activeInHierarchy)
            {
                UIdeath.instance.leaderboard.SetActive(false);
            }
            else
            {
                ShowLeaderboard();
            }
        }

    }
    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code < 200)
        {
            EventCodes theEvent = (EventCodes)photonEvent.Code;
            object[] data = (object[])photonEvent.CustomData;

            //Debug.Log("Received event " + theEvent);
            switch (theEvent)
            {
                case EventCodes.NewPlayer:

                    NewPlayerReceive(data);

                    break;

                case EventCodes.ListPlayers:

                    ListPlayersReceive(data);

                    break;

                case EventCodes.UpdateStat:

                    UpdateStatsReceive(data);

                    break;

                case EventCodes.NextMatch:

                    // NextMatchReceive();

                    break;

                case EventCodes.TimerSync:

                    //TimerReceive(data);

                    break;
            }
        }
    }
    public void NewPlayerSend(string username)
    {
        object[] package = new object[4];
        package[0] = username;
        package[1] = PhotonNetwork.LocalPlayer.ActorNumber;
        package[2] = 0;
        package[3] = 0;


        PhotonNetwork.RaiseEvent(
            (byte)EventCodes.NewPlayer,
            package,
            new RaiseEventOptions { Receivers = ReceiverGroup.MasterClient },
            new SendOptions { Reliability = true }
            );
    }

    public void NewPlayerReceive(object[] dataReceived)
    {
        PlayerInfo player = new PlayerInfo(
            (string)dataReceived[0], 
            (int)dataReceived[1], 
            (int)dataReceived[2], 
            (int)dataReceived[3]);

        allPlayers.Add(player);

        ListPlayersSend();
    }

    public void UpdateStatsSend(int actorSending, int statToUpdate, int amountToChange)
    {
        object[] package = new object[] { actorSending, statToUpdate, amountToChange };

        PhotonNetwork.RaiseEvent(
            (byte)EventCodes.UpdateStat,
            package,
            new RaiseEventOptions { Receivers = ReceiverGroup.All },
            new SendOptions { Reliability = true }
            );
    }

    public void UpdateStatsReceive(object[] dataReceived)
    {
        int actor = (int)dataReceived[0];
        int statType = (int)dataReceived[1];
        int amount = (int)dataReceived[2];

        for (int i = 0; i < allPlayers.Count; i++)
        {
            if (allPlayers[i].actor == actor)
            {
                switch (statType)
                {
                    case 0: //kills
                        allPlayers[i].kills += amount;
                        Debug.Log("Player " + allPlayers[i].name + " : kills " + allPlayers[i].kills);
                        break;

                    case 1: //deaths
                        allPlayers[i].deaths += amount;
                        Debug.Log("Player " + allPlayers[i].name + " : deaths " + allPlayers[i].deaths);
                        break;
                }

               if (i == index)
                {
                    UpdateStatsDisplay();
                }

                if (UIdeath.instance.leaderboard.activeInHierarchy)
                {
                    ShowLeaderboard();
                }

                break;
            }
        }

       // ScoreCheck();
    }


    public void ListPlayersSend()
    {
        object[] package = new object[allPlayers.Count + 1];

        package[0] = state;

        for (int i = 0; i < allPlayers.Count; i++)
        {
            object[] piece = new object[4];

            piece[0] = allPlayers[i].name;
            piece[1] = allPlayers[i].actor;
            piece[2] = allPlayers[i].kills;
            piece[3] = allPlayers[i].deaths;

            package[i + 1] = piece;
        }

        PhotonNetwork.RaiseEvent(
            (byte)EventCodes.ListPlayers,
            package,
            new RaiseEventOptions { Receivers = ReceiverGroup.All },
            new SendOptions { Reliability = true }
            );
    }

    public void ListPlayersReceive(object[] dataReceived)
    {
        allPlayers.Clear();

        state = (GameState)dataReceived[0];

        for (int i = 1; i < dataReceived.Length; i++)
        {
            object[] piece = (object[])dataReceived[i];

            PlayerInfo player = new PlayerInfo(
                (string)piece[0],
                (int)piece[1],
                (int)piece[2],
                (int)piece[3]
                );

            allPlayers.Add(player);

            if (PhotonNetwork.LocalPlayer.ActorNumber == player.actor)
            {
                index = i - 1;
            }
        }

        StateCheck();
    }
   
    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    void StateCheck()
    {

    }
    public void UpdateStatsDisplay()
    {
        if (allPlayers.Count > index)
        {

            LeaderBoard.instance.killsText.text = "Kills: " + allPlayers[index].kills;
            
        }
        else
        {
            LeaderBoard.instance.killsText.text = "Kills: 0";
        }
    }

    void ShowLeaderboard()
    {
        UIdeath.instance.leaderboard.SetActive(true);

        foreach (LeaderBoard lp in lboardPlayers)
        {
            Destroy(lp.gameObject);
        }
        lboardPlayers.Clear();

        UIdeath.instance.leaderboardPlayerDisplay.gameObject.SetActive(false);

        foreach (var player in allPlayers)
        {
            player.kills = (int)PhotonNetwork.CurrentRoom.CustomProperties[player.name+ICommon.CustomProperties_Key_KillsCount()];
            player.deaths = (int)PhotonNetwork.CurrentRoom.CustomProperties[player.name+ICommon.CustomProperties_Key_DeathsCount()];

        }

        List<PlayerInfo> sorted = SortPlayers(allPlayers);

        foreach (PlayerInfo player in sorted)
        {
            LeaderBoard newPlayerDisplay = Instantiate(UIdeath.instance.leaderboardPlayerDisplay, UIdeath.instance.leaderboardPlayerDisplay.transform.parent);

            // int KillsCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[player.name+ICommon.CustomProperties_Key_KillsCount()];
            // int DeathsCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[player.name+ICommon.CustomProperties_Key_DeathsCount()];

            string ExtraInfo = "";
            if (player.name == PhotonNetwork.LocalPlayer.NickName)// Indicate which one is player's
            {
                ExtraInfo = "(Me)";
            }

            newPlayerDisplay.SetDetails(player.name, $"{player.kills}/{player.deaths} {ExtraInfo}");

            newPlayerDisplay.gameObject.SetActive(true);

            lboardPlayers.Add(newPlayerDisplay);
        }
    }
    private List<PlayerInfo> SortPlayers(List<PlayerInfo> players)
    {
        List<PlayerInfo> sorted = new List<PlayerInfo>();

        while (sorted.Count < players.Count)
        {
            int highest = -1;
            PlayerInfo selectedPlayer = players[0];

            foreach (PlayerInfo player in players)
            {
                if (!sorted.Contains(player))
                {
                    if (player.kills > highest)
                    {
                        selectedPlayer = player;
                        highest = player.kills;
                    }
                }
            }

            sorted.Add(selectedPlayer);
        }

        return sorted;
    }

    #region PlayerInfo

    [System.Serializable]
    public class PlayerInfo
    {
        public string name;
        public int actor, kills, deaths;



        public PlayerInfo(string _name, int _actor, int _kills, int _deaths)
        {
            name = _name;
            actor = _actor;
            kills = _kills;
            deaths = _deaths;
        }
    }
    #endregion
}
