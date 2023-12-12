using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class GunSelectionController : MonoBehaviour
{
    public GunDatabase gunDB;

    [Header("Gun Infor")]
    public TextMeshProUGUI gunName;
    public TextMeshProUGUI dame;
    public TextMeshProUGUI fireRate;
    public TextMeshProUGUI ammoCapacity;
    public TextMeshProUGUI bulletSpeed;
    public TextMeshProUGUI reloadTime;

    [Header("Gun Holder")]
    public Transform gunHolder;
    private int selectedOption = 1;

    void Start()
    {
        UpdateGun(selectedOption);
    }

    public void NextOption()
    {
        selectedOption++;
        if (selectedOption >= gunDB.gunCount)
        {
            selectedOption = 0;
        }

        UpdateGun(selectedOption);
    }

    public void BackOption()
    {
        selectedOption--;
        if (selectedOption < 0)
        {
            selectedOption = gunDB.gunCount - 1;
        }

        UpdateGun(selectedOption);
    }

    private void UpdateGun(int selectedOption)
    {
        GunAttribute gun = gunDB.GetGunAttribute(selectedOption);

        if (gunHolder.childCount > 0)
        {
            Destroy(gunHolder.GetChild(0).gameObject);
        }
        GameObject newGunObject = Instantiate(gun.gunObject);
        newGunObject.transform.SetParent(gunHolder, false);

        gunName.text = gun.gunName;
        dame.text = gun.damage.ToString();
        fireRate.text = gun.fireRate.ToString();
        ammoCapacity.text = gun.ammoCapacity.ToString();
        bulletSpeed.text = gun.bulletSpeed.ToString();
        reloadTime.text = gun.reloadTime.ToString();
    }
}

