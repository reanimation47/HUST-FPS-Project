using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenuManager : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    private bool isDeath = false;

    [SerializeField] private GameObject deathMenu;

    private void Start()
    {
        ActivateDeathMenu(false);

    }

    private void Update()
    {
        if (playerHealth.isDeath() == true)
        {
            Debug.Log("Death");
        }
    }

    public void ActivateDeathMenu(bool state)
    {
        deathMenu.SetActive(state);

        Time.timeScale = state ? 0 : 1;
        isDeath = state;
    }
}
