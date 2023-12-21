using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GunDatabase : ScriptableObject
{
    public GunAttribute[] gunsList;

    public int gunCount
    {
        get
        {
            return gunsList.Length;
        }
    }

    public GunAttribute GetGunAttribute(int index)
    {
        return gunsList[index];
    }
}
