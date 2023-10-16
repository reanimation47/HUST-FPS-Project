using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Keypad : Interactable
{
    public GameObject door;
    private bool doorOpen = false;

    protected override void Interact()
    {
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("isOpen", doorOpen);
    }
}
