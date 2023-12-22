using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    private void Awake()
    {
        instance = this; 
    }
    public Transform[] SpawnPoints;
    // Start is called before the first frame update
    void Start()
    { 
        foreach(Transform spawn in SpawnPoints)
        {
            spawn.gameObject.SetActive(true);
        }
    }

    public Transform GetSpawnPoint()
    {
        return SpawnPoints[Random.Range(0, SpawnPoints.Length)];
    }

    //public void RespawnSelf(GameObject target)
    //{
    //    target.SetActive(false);
    //    target.transform.position = GetSpawnPoint().position;
    //    target.SetActive(true);
    //}
}
 }
