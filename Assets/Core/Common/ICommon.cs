using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class ICommon : MonoBehaviour
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

    private static GameObject bulletHole;
    public static void LoadBulletHolePrefab(GameObject hole)
    {
        bulletHole = hole;
    }
    public static void CheckForHits(RaycastHit _hit, float baseDamage)
    {
        if (_hit.transform.gameObject.TryGetComponent(out ICombat combatable)) // If target has combat system
        {
            combatable.TakeDamage(baseDamage);
        }else 
        {
            Instantiate(bulletHole, _hit.point + new Vector3(_hit.normal.x * 0.01f, _hit.normal.y * 0.01f, _hit.normal.z * 0.01f), Quaternion.LookRotation(-_hit.normal));
        }
        if (_hit.transform.gameObject.TryGetComponent(out Rigidbody rb)) // if target has rigidbody
        {
            //combatable.TakeDamage(baseDamage);
            Debug.LogWarning("hittt");
            
            rb.AddForce(-_hit.normal*7000);
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
