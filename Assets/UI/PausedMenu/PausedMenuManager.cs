using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedMenuManager : MonoBehaviour
{
    private bool isPaused = false;

    [SerializeField] private GameObject pausedMenu;
    
    private void Start()
    {
        ActivatePausedMenu(false);

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isPaused)
             ActivatePausedMenu(true);
        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused)
             ActivatePausedMenu(false);    
    }

    public void ActivatePausedMenu(bool state)
    {
        pausedMenu.SetActive(state);

        Time.timeScale = state ? 0 : 1;
        isPaused = state;
    }
}
