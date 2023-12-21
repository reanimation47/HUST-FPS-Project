using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform Goal;
    public Transform Spawn;
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

    public void GoalItemRetrieved(GameObject item)
    {
        ObjectiveItemRetrieved = true;
        ICommon.GetPlayerController().UpdateObjective(); 
        Destroy(item);
    }

}
