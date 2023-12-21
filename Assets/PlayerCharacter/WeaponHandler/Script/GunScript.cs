using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    [HideInInspector] public bool isEquipped = false;
    [HideInInspector] public bool isActive = false;
    public GunID GunID = GunID.DEFAULT;
    public GunType GunType;
    public GameObject GunSpawn;
    public float baseDamage = 30;
    public float fireRate = 0.15f;
    public int clipSize = 12;
    public int reservedAmmo = 12*10;
    public float reloadTime = 2f;
    public Vector2 randomRecoilConstraints = new Vector2(2,5);
    public Image muzzleFlash;
    [HideInInspector] public int _currentAmmoInClip;
    [HideInInspector] public int _ammoInReserve;
    private void Awake()
    {
        //_currentAmmoInClip = clipSize;
        //_ammoInReserve = reservedAmmo;
        ICommon.LoadGunHolder(this);
        if(GunType == GunType.PISTOL)//Default weapon
        {
            ICommon.ActiveGunHolder(this);
            _currentAmmoInClip = clipSize;
            _ammoInReserve = reservedAmmo;
        }
    }

    private void Start()
    {
    }

    public void SpawnGun(GameObject gun)
    {
        gun.transform.localScale = new Vector3(1,1,1);
        Instantiate(gun, GunSpawn.transform);
    }

    public void LoadGunStats(GunAttribute stats)
    {
        baseDamage = stats.damage;
        fireRate = stats.fireRate;
        reloadTime = stats.reloadTime;
        randomRecoilConstraints = new Vector2(stats.recoil, stats.recoil);
        clipSize = stats.ammoCapacity;
        reservedAmmo = clipSize * 7;

        _currentAmmoInClip = clipSize;
        _ammoInReserve = reservedAmmo;
    }
}
