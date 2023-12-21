using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UserDatabase : ScriptableObject
{
    public UserAttribute UserInformation;

    public int gunSkinOwnedCount
    {
        get
        {
            // Check if UserInformation is not null before accessing its properties
            return UserInformation != null ? UserInformation.skinOwned.Length : 0;
        }
    }

    public GunSkin GetGunSkinOwnedAttribute(int index)
    {
        // Check if UserInformation is not null and if the index is within bounds
        if (UserInformation != null && index >= 0 && index < UserInformation.skinOwned.Length)
        {
            return UserInformation.skinOwned[index];
        }
        else
        {
            // Handle the case where the index is out of bounds or UserInformation is null
            // You might want to log an error or return a default GunSkin in such cases
            return null; // or return a default GunSkin instance
        }
    }

    public UserAttribute UserOwned
    {
        get
        {
            return UserInformation; // Return the entire UserInformation
        }
    }

    public int coinOwned
    {
        get
        {
            return coinOwned;
        }
    }
}
