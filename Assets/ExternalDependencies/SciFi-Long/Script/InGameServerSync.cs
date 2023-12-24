using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InGameServerSync : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SyncOtherPlayers();
        
    }

    private void SyncOtherPlayers()
    {
        GameObject[] AllPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach(var p in AllPlayers)
        {
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
}
