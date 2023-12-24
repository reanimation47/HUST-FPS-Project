using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject PVPScene;
    [SerializeField] private GameObject PVEScene;
    [SerializeField] private GameObject Map1Scene;
    [SerializeField] private GameObject Map2Scene;
    [SerializeField] private GameObject ShopScene;
    [SerializeField] private GameObject SettingScene;

    private void Start()
    {
        ActivateMainMenuPVP(true);
        ActivateMainMenuPVE(true);
    }

    public void ActivateMainMenuPVP(bool state)
    {
        mainMenu.SetActive(state);
        PVPScene.SetActive(!state);
    }

    public void ActivateMainMenuPVE(bool state)
    {
        mainMenu.SetActive(state);
        PVEScene.SetActive(!state);
    }

    public void ActivateMap1(bool state)
    {
        PVEScene.SetActive(state);
        Map1Scene.SetActive(!state);
    }

    public void ActivateMap2(bool state)
    {
        PVEScene.SetActive(state);
        Map2Scene.SetActive(!state);
    }

    public void ActivateShop(bool state)
    {
        mainMenu.SetActive(state);
        ShopScene.SetActive(!state);
    }

    public void ActivateSetting(bool state)
    {
        mainMenu.SetActive(state);
        SettingScene.SetActive(!state);
    }
}