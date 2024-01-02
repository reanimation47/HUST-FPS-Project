using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform Goal;
    public Transform Spawn;
    public GameObject[] Waves;
    private int WaveIndex = 0;
    public bool respawnEnabled = true;
    public bool ObjectiveItemRetrieved = false;
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
    void Start()
    {
        NextWave();
    }

    public void GoalItemRetrieved(GameObject item)
    {
        ObjectiveItemRetrieved = true;
        ICommon.GetPlayerController().UpdateObjective(); 
        NextWave();
        item.SetActive(false);
    }

    public void MissionCompleted()
    {
        Debug.LogWarning("MISSION COMPLETE");
        ICommon.UpdatePlayerCoinsBalance(+1000);
        //TODO: switch to Extraction Complete screen ()
        MainMenu.Instance.MissionSuccess.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

    }

    private void NextWave()
    {
        if (WaveIndex >0)
        {
            Waves[WaveIndex-1].SetActive(false);
        }
        Waves[WaveIndex].SetActive(true);
        WaveIndex++;
    }

}
