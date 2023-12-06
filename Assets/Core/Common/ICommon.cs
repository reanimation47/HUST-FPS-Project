using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public interface ICommon 
{


    #region Player
    private static GameObject _player;
    public static void LoadPlayer(GameObject player)
    {
        _player = player;
    }

    public static PlayerController GetPlayerController()
    {
        return _player.GetComponent<PlayerController>();
    }

    public static GameObject GetPlayerGameObject()
    {
        return _player;
    }
    #endregion

    #region Combat
    public static void CheckForHits(RaycastHit _hit, float baseDamage)
    {
        if (_hit.transform.gameObject.TryGetComponent(out ICombat combatable)) // is this even a word
        {
            combatable.TakeDamage(baseDamage);
        }

    }
    #endregion

    #region Templates - Unused
    public static int GetGunPrice(int index)
    {
        return index * 500;
    }

    public static void SetPlayerStartingGun(int index)
    {
        //
    }

    public static bool PlayerPurchaseGun(int index)
    {
        return true;
    }

    public static int GetPlayerBalance()
    {
        return 10000;
    }
    #endregion





}
