using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunAttribute
{
    public GameObject gunObject;
    public GunSkin [] gunSkin;
    public string gunName;
    public GunType gunType;
    public int damage;
    public float fireRate;
    public int ammoCapacity;
    public float bulletSpeed;//Not gonna use this in game// consider remove & replace with recoil in gun shop
    public float recoil;
    public float reloadTime;

    public AudioClip reloadSFX;
    public AudioClip shootingSFX;
}
