using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using System.Text.RegularExpressions;

public class InGameServerSync : MonoBehaviour
{
    public GameObject DeathCam;//In this case game over cam
    public GameObject LeaderBoard;
    public GameObject EndGameMsg;
    public TextMeshProUGUI EngGameMsg_Text;
   

    // Update is called once per frame
    void FixedUpdate()
    {
        SyncOtherPlayers();
        
    }

    private void SyncOtherPlayers()
    {
        GameObject[] AllPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach(var p in AllPlayers)
        {
            CheckForGameOver(p);
            if(!p.GetComponent<PhotonView>().IsMine)
            {
                RenderFullPlayerObject(p);
            }

        }

    }
    
    private void RenderFullPlayerObject(GameObject p)
    {
        if(p.layer == 9){return;}
        Debug.LogWarning("LayerUpdated!!");
        ICommon.SetLayerAllChildren(p.transform,9);
    }

    private void CheckForGameOver(GameObject p)
    {
        PhotonView view = p.GetComponent<PhotonView>();
        int KillsCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[view.Owner.NickName+ICommon.CustomProperties_Key_KillsCount()];
        if (KillsCount >= MatchManager.instance.TargetKills)
        {
            ICommon.GetPlayerController().EnableCamera(false);
            MatchManager.instance.state = MatchManager.GameState.Ending;
            DeathCam.SetActive(true);
            EndGameMsg.SetActive(true);
            EngGameMsg_Text.text = $"GAME OVER! THE WINNER IS: {view.Owner.NickName}";
            MatchManager.instance.ShowLeaderboard();
            MatchManager.instance.EndGame();
        }

    }
}
