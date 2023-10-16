using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Components
{
    public class PlayerInteract
    {
        private static Camera _camera;
        private static bool isWithinRangeToInteract = false;
        private static Interactable _interactable;

        public static void AssignCamera(Camera cam)
        {
            _camera = cam;
        }

        public static void CheckInteraction()
        {
            PlayerUI.UpdateText(string.Empty);
            isWithinRangeToInteract = false;

            Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * PlayerController.interactDistance);
            RaycastHit hitInfo;
            
            if (Physics.Raycast(ray, out hitInfo, PlayerController.interactDistance, PlayerController.interactMask))
            {
                _interactable = hitInfo.collider.GetComponent<Interactable>();
                PlayerUI.UpdateText(_interactable.promptMessage);
                isWithinRangeToInteract = true;
            }
        }

        public static void Interact()
        {
            if (isWithinRangeToInteract)
            {
                _interactable.BaseInteract();
            }
        }
    }
}


