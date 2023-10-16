using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Player.Components
{
    public class PlayerUI
    {
        public static void UpdateText(string promptMessage)
        {
            PlayerController.promptMessage.text = promptMessage;
        }
    }
}


