using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Player.Components
{
    public class PlayerUI : MonoBehaviour
    {
        public static PlayerUI instance;

        private void Awake()
        {
           instance = this;
        }
        public static void UpdateText(string promptMessage)
        {
            PlayerController.promptMessage.text = promptMessage;
        }

        public static void UpdateObjective()
        {
            if (GameManager.Instance.ObjectiveItemRetrieved)
            {
                PlayerController.objectiveMessage.text = "Item retrieved! Now return to safety";
            }else
            {
                PlayerController.objectiveMessage.text = "Objective: Find and retrieve the ITEM";
            }
        }
    }
}


