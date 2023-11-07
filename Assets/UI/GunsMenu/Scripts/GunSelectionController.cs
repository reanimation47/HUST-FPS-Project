using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSelectionController : MonoBehaviour
{
    public GameObject[] GunsList;

    private int currentGunIndex = -1;

    public void NextGun()
    {
        //Disable all guns
        foreach (GameObject gun in GunsList)
        {
            gun.SetActive(false);
        }
        currentGunIndex += 1;
        GunsList[currentGunIndex].SetActive(true);
    }

}
