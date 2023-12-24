using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenuManager : MonoBehaviour
{
    private bool isPaused = false;

    [SerializeField] private GameObject pausedMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject Map1Scene;
    
    private void Start()
    {
        ActivatePausedMenu(false);

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && !isPaused)
             ActivatePausedMenu(true);
        else if(Input.GetKeyDown(KeyCode.P) && isPaused)
             ActivatePausedMenu(false);    
    }

    public void ActivatePausedMenu(bool state)
    {
        pausedMenu.SetActive(state);

        Time.timeScale = state ? 0 : 1;
        isPaused = state;
    }
    
    public void ActivateResume(bool state)
    {
        pausedMenu.SetActive(state);
        Map1Scene.SetActive(!state);
    }

    public void ActivateBackToMenu(bool state)
    {
        pausedMenu.SetActive(state);
        mainMenu.SetActive(!state);
    }

    public void ActivateQuit(bool state)
    {
        Application.Quit();
    }
    
}
