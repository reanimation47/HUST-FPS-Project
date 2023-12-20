using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class GunSelectionController : MonoBehaviour
{
    public GunDatabase gunDB;
    public GunSkinDatabase gunSkinDB;

    [Header("Gun Info")]
    public TextMeshProUGUI gunName;
    public TextMeshProUGUI dame;
    public TextMeshProUGUI fireRate;
    public TextMeshProUGUI ammoCapacity;
    public TextMeshProUGUI bulletSpeed;
    public TextMeshProUGUI reloadTime;

    [Header("Gun Holder")]
    public Transform gunHolder;
    private int selectedOption = 1;

    [Header("Gun Skins")]
    public Transform[] skinHolder;
    public TextMeshProUGUI price;


    void Start()
    {
        UpdateGun(selectedOption);
    }

    public void NextOption()
    {
        selectedOption = (selectedOption + 1) % gunDB.gunCount;
        UpdateGun(selectedOption);
    }

    public void BackOption()
    {
        selectedOption = (selectedOption - 1 + gunDB.gunCount) % gunDB.gunCount;
        UpdateGun(selectedOption);
    }

    private void UpdateGun(int selectedOption)
    {
        GunAttribute gun = gunDB.GetGunAttribute(selectedOption);

        // Update Gun
        if (gunHolder.childCount > 0)
        {
            Destroy(gunHolder.GetChild(0).gameObject);
        }
        GameObject newGunObject = Instantiate(gun.gunObject);
        newGunObject.transform.SetParent(gunHolder, false);

        // Update Gun Skins
        for (int i = 0; i < skinHolder.Length; i++)
        {
            Transform currentSkinHolder = skinHolder[i];

            if (currentSkinHolder.childCount > 0)
            {
                Destroy(currentSkinHolder.GetChild(0).gameObject);
            }

            if (i < gun.gunSkin.Length)
            {
                GameObject newGunSkinObject = Instantiate(gun.gunSkin[i]);
                newGunSkinObject.transform.SetParent(currentSkinHolder, false);
            }
            else
            {
                // Handle the case where there are fewer gun skins than expected
                UnityEngine.Debug.LogError("Not enough gun skins for index: " + i);
            }
        }

        // Update UI Text
        gunName.text = gun.gunName;
        dame.text = gun.damage.ToString();
        fireRate.text = gun.fireRate.ToString();
        ammoCapacity.text = gun.ammoCapacity.ToString();
        bulletSpeed.text = gun.bulletSpeed.ToString();
        reloadTime.text = gun.reloadTime.ToString();
    }

    public void selectSkin(int selectIndex)
    {
        UnityEngine.Debug.Log(selectIndex);
        GunAttribute gun = gunDB.GetGunAttribute(selectedOption);
       
        if (selectIndex >= 0 && selectIndex < gun.gunSkin.Length)
        {
            if (gunHolder.childCount > 0)
            {
                Destroy(gunHolder.GetChild(0).gameObject);
            }

            GameObject newGunSkinObject = Instantiate(gun.gunSkin[selectIndex]);
            newGunSkinObject.transform.SetParent(gunHolder, false);
        }
        else
        {
            // Handle invalid index
            UnityEngine.Debug.LogError("Invalid gun skin index: " + selectIndex);
        }
    }

}
