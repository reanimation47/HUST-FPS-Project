using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Components
{
    public class PlayerBodyAnimation : MonoBehaviour
    {
        public static void SyncCharacterHeadRotation(float _rotation)
        {
            //PlayerController.characterHead.transform.localRotation = Quaternion.Euler(0, 0, -_rotation);
            PlayerController.bodySpine.transform.localRotation = Quaternion.Euler(0, 0, -_rotation);
        }

        public static void ProcessMovementAnimation(Vector2 input)
        {
            bool isMoving = input.magnitude > 0 ? true : false;
            PlayerController.characterAnimator.SetBool("IsMoving", isMoving);
        }
    }

}


