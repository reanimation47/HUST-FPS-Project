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

    public void ActivateMap(bool state)
    {
        PVEScene.SetActive(state);
        Map1Scene.SetActive(!state);
    }

}