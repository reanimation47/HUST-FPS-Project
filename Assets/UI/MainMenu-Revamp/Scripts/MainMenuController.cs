using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private SceneNames sceneNames;
    void Start()
    {
        sceneNames = new SceneNames();
    }
    public void OpenPVPLobby()
    {
        StartCoroutine(LoadMap(sceneNames.PVP_Lobby));
    }

    public void OpenPVEMapSelect()
    {
        
        StartCoroutine(LoadMap(sceneNames.PvE_MapSelect));
    }

    public void OpenWeaponsShop()
    {
        StartCoroutine(LoadMap(sceneNames.WeaponsShop));
    }

    public void BackToMainMenu()
    {
        StartCoroutine(LoadMap(sceneNames.MainMenu));
    }

    public void StartPVEMap1()
    {
        PvESceneNames pvESceneNames = new PvESceneNames();
        StartCoroutine(LoadMap(pvESceneNames.Map1));
    }
    public void StartPVEMap2()
    {
        PvESceneNames pvESceneNames = new PvESceneNames();
        StartCoroutine(LoadMap(pvESceneNames.Map2));
    }


    IEnumerator LoadMap(string sceneName)
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
