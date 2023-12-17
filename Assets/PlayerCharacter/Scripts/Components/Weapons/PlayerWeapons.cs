using System.Collections;
using System.Collections.Generic;
using Player.WeaponHandler;
using UnityEngine;
using TMPro;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI AmmoText;
    [SerializeField] private GameObject weaponsHolder;

    [HideInInspector] public List<GunScript> _guns;

    private GunScript currentActiveGun;
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
        //EnableEquippedGuns();
        //ICommon.CleanupUnEquippedGuns();
        ICommon.EnableEquippedGuns(equippedGuns);
        _guns = ICommon.GetEquippedGuns();
        EquipGun(1);
    }

    private void Update()
    {
        //UpdateCurrentAmmo();
    }

    private void UpdateCurrentAmmo()
    {
        AmmoText.text = $"{currentActiveGun._currentAmmoInClip}/{currentActiveGun._ammoInReserve}";
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

    public void EquipGun(int slotIndex)
    {
        Debug.LogWarning(_guns.Count);
        for (int i = 0; i < _guns.Count; i++)
        {
            var gun = _guns[i];
            if (i == slotIndex)
            {
                Debug.LogWarning(gun.GunID);
                gun.transform.gameObject.SetActive(true);
                currentActiveGun = gun;
            }else
            {
                gun.transform.gameObject.SetActive(false);
            }
        }
    }
}

