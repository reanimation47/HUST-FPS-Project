using Player;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject deathCam;
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

    public void SpawnPlayer(bool isRespawn = false)
    {
        Transform spawnPoint = SpawnManager.instance.GetSpawnPoint();

        if(isRespawn)
        {
            player.transform.position = spawnPoint.position;
            player.SetActive(true);
            player.GetComponent<PlayerController>().ResetStats();
        }else
        {
            player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
        }
        player.GetComponent<MultiplayerSetup>().SetupForLocal();
    }

    public void Die()
    {


        UIdeath.instance.deathText.text = "You were killed";

        //PhotonNetwork.Destroy(player);
        // player.SetActive(false);

        // SpawnPlayer();

        //MatchManager.instance.UpdateStatsSend(PhotonNetwork.LocalPlayer.ActorNumber, 1, 1);

       if (player != null)
        {
            StartCoroutine(DieCo());
        }
    }

    
        public IEnumerator DieCo()
        {

            //PhotonNetwork.Destroy(player);
            player.SetActive(false);
            yield return new WaitForSeconds(0.01f);
            player.transform.position = new Vector3(1000,-100,1000);//quick fix because after deactivation the player object still visible for player on another player, 
            deathCam.SetActive(true);
            //player = null;
            UIdeath.instance.deathScreen.SetActive(true);

            yield return new WaitForSeconds(respawnTime);

            UIdeath.instance.deathScreen.SetActive(false);
            deathCam.SetActive(false);

            SpawnPlayer(true);
    }

}
