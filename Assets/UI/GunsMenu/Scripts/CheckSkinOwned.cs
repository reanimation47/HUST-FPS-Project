using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CheckSkinOwned
{
    // Phương thức chuyển đổi mảng thành chuỗi
    string ArrayToString(List<string> list)
    {
        return string.Join(",", list);
    }

    // Phương thức chuyển đổi chuỗi thành mảng
    List<string> StringToArray(string arrayString)
    {
        string[] stringArray = arrayString.Split(',');
        List<string> list = new List<string>(stringArray);
        return list;
    }

    public void SaveOwnedGunSkin(string skinName)
    {   
        List<string> gunSkinOwnedList = StringToArray(PlayerPrefs.GetString("UserOwnedGunSkin"));
        // Add the new skin name to the list if it doesn't exist
        if (!gunSkinOwnedList.Contains(skinName))
        {
            gunSkinOwnedList.Add(skinName);
        }

        string dataSkinOwned = ArrayToString(gunSkinOwnedList);

        PlayerPrefs.SetString("UserOwnedGunSkin", dataSkinOwned);
    }

    public List<string> GetSkinNames()
    {
        string retrievedString = PlayerPrefs.GetString("UserOwnedGunSkin");
        return StringToArray(retrievedString);
    }

    public Boolean isOwnedSkin (string skinName)
    {
        List<string> gunSkinOwnedList = StringToArray(PlayerPrefs.GetString("UserOwnedGunSkin"));
        // Add the new skin name to the list if it doesn't exist
        if (gunSkinOwnedList.Contains(skinName))
        {
            return true;
        } else
        {
            return false;
        }
    }

}

