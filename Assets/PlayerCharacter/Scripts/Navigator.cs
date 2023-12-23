using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class Navigator : MonoBehaviour
{

    // Update is called once per frame
    PlayerController playerController;
    Transform Goal;
    Transform Spawn;
    void Start()
    {
        playerController = ICommon.GetPlayerController();
        if(playerController.gameMode == GameMode.Multiplayer) {return;}
        Goal = GameManager.Instance.Goal;
        Spawn = GameManager.Instance.Spawn;
    }
    void Update()
    {
        if(playerController.gameMode == GameMode.Multiplayer) {return;}
        if(GameManager.Instance.ObjectiveItemRetrieved)
        {
            transform.LookAt(Spawn);

        }else
        {
            transform.LookAt(Goal);
        }
    }
}
