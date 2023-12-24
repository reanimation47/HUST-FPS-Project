using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using Player;
using Player.WeaponHandler;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine;
using UnityEngine.InputSystem.HID;

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
            // PhotonView hitview = _hit.transform.gameObject.GetComponent<PhotonView>();
            // _hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", hitview.Controller, baseDamage);
            if(_player.GetComponent<PlayerController>().gameMode == GameMode.Multiplayer)
            {
                PhotonView hitview = _hit.transform.gameObject.GetComponent<PhotonView>();

                float targetHP = (float)PhotonNetwork.CurrentRoom.CustomProperties[hitview.Owner.NickName];
                if (_hit.collider.gameObject.tag == "Player")
                {
                    Debug.Log("Hit " + _hit.collider.gameObject.GetPhotonView().Owner.NickName);

                    PhotonNetwork.Instantiate(_player.name, _hit.point, Quaternion.identity);

                    _hit.collider.gameObject.GetPhotonView().RPC("DealDamage", RpcTarget.All, hitview.Owner.NickName, PhotonNetwork.LocalPlayer.ActorNumber);
                }
                targetHP -= baseDamage;
                if(targetHP <= 0)
                {
                    Debug.LogWarning("Killed " +hitview.Owner.NickName);
                    
                }
                Hashtable hash = new Hashtable();
                hash.Add(hitview.Owner.NickName, targetHP);
                PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
                //_hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", hitview.Controller, baseDamage);
                //CheckForTargetHP(hitview.Owner.NickName);
            }else
            {
                combatable.TakeDamage(baseDamage);
            }
        }else 
        {
            Debug.LogError(_hit.transform.gameObject.name);
            Instantiate(bulletHole, _hit.point + new Vector3(_hit.normal.x * 0.01f, _hit.normal.y * 0.01f, _hit.normal.z * 0.01f), Quaternion.LookRotation(-_hit.normal));
        }
        if (_hit.transform.gameObject.TryGetComponent(out Rigidbody rb)) // if target has rigidbody
        {
            //combatable.TakeDamage(baseDamage);
            //Debug.LogWarning("hittt");
            
            rb.AddForce(-_hit.normal*7000); //TODO: Maybe the hit force could scale with the weapon's dmg?
        }

    }
    
    private static void CheckForTargetHP(string targetName)
    {
        Debug.LogWarning(targetName);
        float targetHP = (float)PhotonNetwork.CurrentRoom.CustomProperties[targetName];
        Debug.LogWarning(targetHP);

    }

    IEnumerator DeactiveObjec(GameObject o, float duration)
    {
        o.SetActive(false);
        yield return new WaitForSeconds(duration);
        o.SetActive(true);

    }
    #endregion

    #region Guns system - OLD
    public static List<BaseGunController> _gunControllers = new List<BaseGunController>();
    public static void LoadGunController(BaseGunController controller)
    {
        if (controller.GunID == GunID.DEFAULT)
        {
            Debug.LogWarning("A non-identifiable gun just tried to load into ICommon, please check to make sure all guns has its GunID field selected.");
        }else
        {
            Debug.Log("ICommon: Added " + controller.GunID);
            _gunControllers.Add(controller);
        }
    }

    public static void EnableGun(GunID id)
    {
        foreach (var controller in _gunControllers)
        {
            if (controller.GunID == id)
            {
                //DestroyImmediate(controller.transform.gameObject); //Remove un-equipped guns
                controller.isEquipped = true;
            }
        }

    }

    public static void CleanupUnEquippedGuns()
    {
        foreach (var controller in _gunControllers)
        {
            if (controller.isEquipped == false)
            {
                DestroyImmediate(controller.transform.gameObject); //Remove un-equipped guns
                //_gunControllers.Remove(controller);
            }
        }
    }

    public static List<BaseGunController> GetEquippedGunControllers()
    {
        return _gunControllers;
    }
    #endregion

    #region Guns System - NEW

    public static List<GunScript> _gunHolders = new List<GunScript>();
    public static List<GunScript> _gunHoldersActive = new List<GunScript>();

    public static List<GunScript> equippedGuns = new List<GunScript>();

    public static void LoadGunHolder(GunScript holder)
    {
        _gunHolders.Add(holder);
    }

    public static List<GunScript> GetLoadedGunHolders()
    {
        return _gunHolders;
    }


    public static void ActiveGunHolder(GunScript holder)
    {
        _gunHoldersActive.Add(holder);
    }

    public static List<GunScript> GetActiveGunHolders()
    {
        return _gunHoldersActive;
    }
    public static List<GunScript> GetEquippedGuns()
    {
        return equippedGuns;
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

    #region Others

    public static void UpdatePlayerCoinsBalance(int amount)
    {
        int _ = PlayerPrefs.GetInt("CoinOwned", 0);

        _ += amount;
        PlayerPrefs.SetInt("CoinOwned", _);
    }
    public static void RemoveObjectFromAnimator(GameObject gameObject, Animator animator)
    {
        Transform parentTransform = gameObject.transform.parent;
        
        gameObject.transform.parent = null;

        float playbackTime = animator.playbackTime;
        
        animator.Rebind ();
        
        animator.playbackTime = playbackTime;

        gameObject.transform.parent = parentTransform;
    }

    public static void SetLayerAllChildren(Transform root, int layer)
    {
        var children = root.GetComponentsInChildren<Transform>(includeInactive: true);
        foreach (var child in children)
        {
            child.gameObject.layer = layer;
        }
    }
    #endregion




}
