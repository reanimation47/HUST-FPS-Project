using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Components
{
    public class PlayerInteract
    {
        private static Camera _camera;

        public static void AssignCamera(Camera cam)
        {
            _camera = cam;
        }

        public static void CheckInteraction()
        {
            PlayerUI.UpdateText(string.Empty);
            Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * PlayerController.interactDistance);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, PlayerController.interactDistance, PlayerController.interactMask))
            {
                PlayerUI.UpdateText(hitInfo.collider.GetComponent<Interactable>().promptMessage);
            }
        }
    }
}


