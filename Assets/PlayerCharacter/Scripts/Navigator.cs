using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour
{

    // Update is called once per frame
    Transform Goal;
    Transform Spawn;
    void Start()
    {
        Goal = GameManager.Instance.Goal;
        Spawn = GameManager.Instance.Spawn;
    }
    // void Update()
    // {
    //     if(GameManager.Instance.ObjectiveItemRetrieved)
    //     {
    //         transform.LookAt(Spawn);

    //     }else
    //     {
    //         transform.LookAt(Goal);
    //     }
    // }
}
