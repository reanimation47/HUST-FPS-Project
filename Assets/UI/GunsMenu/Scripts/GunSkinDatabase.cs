using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GunSkinDatabase : ScriptableObject
{
    public GunSkin[] gunSkinList;

    public int gunSkinCount
    {
        get
        {
            return gunSkinList.Length;
        }
    }

    public GunSkin GetGunSkinAttribute(int index)
    {
        return gunSkinList[index];
    }
}
