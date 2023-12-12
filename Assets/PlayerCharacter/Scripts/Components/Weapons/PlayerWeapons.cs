using System.Collections;
using System.Collections.Generic;
using Player.WeaponHandler;
using UnityEngine;
using UnityEngine.Scripting;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] private GameObject weaponsHolder;
    public List<GunID> equippedGuns = new List<GunID>() // TODO: Sync this list with guns equipped from Guns Menu
    {
        GunID.AK47_01,
        GunID.GLOCK_01,
    };

    private void Awake()
    {
    }

    private void Start()
    {
        EnableEquippedGuns();
        ICommon.CleanupUnEquippedGuns();
    }

    private void EnableEquippedGuns()
    {
        if (equippedGuns.Count > 2)
        {
            Debug.LogWarning("Equipped guns count is not expected to be more than 2.");
        }

        foreach (var gunID in equippedGuns)
        {
            Debug.LogWarning(gunID);
            ICommon.EnableGun(gunID);
        }
    }

}

