using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject lobby;
    [SerializeField] private GameObject inviteMenu;
    private void Start()
    {
        ActivateLobby(true);

    }
    public void ActivateLobby(bool state)
    {
        lobby.SetActive(state);
        inviteMenu.SetActive(!state);
    }
}

//    public void PLay()
//    {
//        SceneManager.LoadScene(1);
//    }

//    public void Quit()
//    {
//        Application.Quit();
//    }
