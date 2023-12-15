using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    private void Start()
    {
        ActivateMainMenu(true);

    }
    public void ActivateMainMenu(bool state)
    {
        mainMenu.SetActive(state);
        optionsMenu.SetActive(!state);
    }

    public void PLay()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
