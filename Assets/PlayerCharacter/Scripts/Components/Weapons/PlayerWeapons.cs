using System.Collections;
using System.Collections.Generic;
using Player.WeaponHandler;
using UnityEngine;
using TMPro;

public class PlayerWeapons : MonoBehaviour
{
    public static PlayerWeapons Instance;

    [SerializeField] private BaseGunController gunController;
    [SerializeField] private TextMeshProUGUI AmmoText;
    [SerializeField] private GameObject weaponsHolder;

    [HideInInspector] public List<GunScript> _guns;

    public int random = 30;

    public GunScript currentActiveGun;
    public List<GunID> equippedGuns = new List<GunID>() // TODO: Sync this list with guns equipped from Guns Menu
    {
        GunID.AK47_02,
        GunID.GLOCK_01,
    };

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


    private void Start()
    {
        //EnableEquippedGuns();
        //ICommon.CleanupUnEquippedGuns();
        ICommon.EnableEquippedGuns(equippedGuns);
        _guns = ICommon.GetEquippedGuns();
        EquipGun(1, true);
        gunController.ManualStart();
    }

    private void Update()
    {
        UpdateCurrentAmmo();
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

    public void EquipGun(int slotIndex, bool atIntro = false)
    {
        if (gunController._isReloading) {return;} //TODO: able to switch gun to stop/skip reloading
        //Debug.LogWarning(_guns.Count);
        if (!atIntro)
        {
            gunController.GunSwitchAnimation();

        }
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

