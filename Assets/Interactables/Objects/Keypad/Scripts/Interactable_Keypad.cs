using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Keypad : Interactable
{
    protected override void Interact()
    {
        Debug.Log("Clicked" + gameObject.name); 
    }
}
