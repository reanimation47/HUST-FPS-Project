using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class MultiplayerSetup : MonoBehaviour
{
    public void SetupForLocal()
    {
        GetComponent<PlayerController>().enabled = true;
        GetComponent<PlayerController>().EnableCamera();
        GetComponent<InputManager>().enabled = true;
        GetComponent<PlayerHealth>().enabled = true;
        GetComponent<PlayerHealth>().HUD.SetActive(true); 
        GetComponent<PlayerWeapons>().enabled = true;
        //GetComponent<PlayerController>().ResetStats();

    }
}
