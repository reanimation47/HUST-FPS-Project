using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.SceneManagement;

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
    
//    public void Restart()
//    {
//        SceneManagement.LoadScene("SampleScene");
//    }

//    public void BackToMenu()
//    {
//        SceneManagement.LoadScene("MainMenu");
//    }

    public void Quit()
    {
        Application.Quit();
    }
    
}
