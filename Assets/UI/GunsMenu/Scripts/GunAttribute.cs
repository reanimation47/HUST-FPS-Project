using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunAttribute
{
    public GameObject gunObject;
    public GameObject [] gunSkin;
    public string gunName;
    public int damage;
    public float fireRate;
    public int ammoCapacity;
    public float bulletSpeed;
    public float reloadTime;
}
