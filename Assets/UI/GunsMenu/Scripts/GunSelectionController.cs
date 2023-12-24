using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

public class GunSelectionController : MonoBehaviour
{
    public GunDatabase gunDB;
    public GunSkinDatabase gunSkinDB;
    public CheckSkinOwned skinOwnedChecker;
    public UserController userController;

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

    [Header("Gun Selected")]
    public TextMeshProUGUI gunSelectedText;

    private int selectedIndexSkin = -1;

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
        string gunNameSelected = gun.gunObject.ToString();

        string gunSelectedKey = "GunSelected";
        string gunSelected = PlayerPrefs.GetString(gunSelectedKey);
        if (gunSelected == gunNameSelected)
        {
            gunSelectedText.text = "EQUIPPED";
        }
        else
        {
            gunSelectedText.text = "NOT-EQUIPPED";
        }
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
            selectedIndexSkin = selectIndex;
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


    public void selectedGun()
    {
        GunAttribute gun = gunDB.GetGunAttribute(selectedOption);
        string gunNameSelected = gun.gunObject.ToString();

        string gunSelectedKey = "GunSelected";
        PlayerPrefs.SetString(gunSelectedKey, gunNameSelected);
        string gunSelected = PlayerPrefs.GetString(gunSelectedKey);
        if (gunSelected == gunNameSelected)
        {
            gunSelectedText.text = "EQUIPPED";
        }
        else
        {
            gunSelectedText.text = "NOT-EQUIPPED";
        }
    }


    public void buyGunSkin()
    {
        UnityEngine.Debug.Log(selectedIndexSkin);

        GunAttribute gun = gunDB.GetGunAttribute(selectedOption);

        UnityEngine.Debug.Log(gun);

        if (gun != null && gun.gunSkin != null && selectedIndexSkin >= 0 && selectedIndexSkin < gun.gunSkin.Length)
        {
            UnityEngine.Debug.Log(gun.gunSkin[selectedIndexSkin].ToString());

            // Make sure skinOwnedChecker is not null before calling its method
            if (skinOwnedChecker != null)
            {
                skinOwnedChecker.SaveOwnedGunSkin(gun.gunSkin[selectedIndexSkin].ToString());
            }
            else
            {
                UnityEngine.Debug.LogError("skinOwnedChecker is null!");
            }

            // Assuming userController is also a valid reference
            userController.UpdateCoinAfterBuying();
        }
        else
        {
            UnityEngine.Debug.LogError("Invalid index or gun attributes!");
        }
    }


}
