using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSelectionController : MonoBehaviour
{
    public GameObject[] GunsList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
