using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [HideInInspector] public bool isEquipped = false;
    [HideInInspector] public bool isActive = false;
    public GunID GunID = GunID.DEFAULT;
    public float baseDamage = 30;
    public float fireRate = 0.1f;
    public int clipSize = 30;
    public int reservedAmmo = 270;
    public Vector2 randomRecoilConstraints = new Vector2(2,5);
    [HideInInspector] public int _currentAmmoInClip;
    [HideInInspector] public int _ammoInReserve;
    private void Awake()
    {
        _currentAmmoInClip = clipSize;
        _ammoInReserve = reservedAmmo;
        ICommon.LoadGun(this);
    }

    private void Start()
    {
    }
}
