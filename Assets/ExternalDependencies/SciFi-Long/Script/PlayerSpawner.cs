using Player;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    public GameObject playerPrefab;
    private GameObject player;


    public float respawnTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            SpawnPlayer();
        }
    }

    public void SpawnPlayer()
    {
        Transform spawnPoint = SpawnManager.instance.GetSpawnPoint();

        player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
        player.GetComponent<MultiplayerSetup>().SetupForLocal();
    }

    public void Die()
    {


        UIdeath.instance.deathText.text = "You were killed";

        PhotonNetwork.Destroy(player);

        SpawnPlayer();

        //MatchManager.instance.UpdateStatsSend(PhotonNetwork.LocalPlayer.ActorNumber, 1, 1);

       if (player != null)
        {
            StartCoroutine(DieCo());
        }
    }

    
        public IEnumerator DieCo()
        {


            PhotonNetwork.Destroy(player);
            player = null;
            UIdeath.instance.deathScreen.SetActive(true);

            yield return new WaitForSeconds(respawnTime);

            UIdeath.instance.deathScreen.SetActive(false);

            SpawnPlayer();
        /*if (MatchManager.instance.state == MatchManager.GameState.Playing && player == null)
        {
            SpawnPlayer();
        }*/
    }

}
