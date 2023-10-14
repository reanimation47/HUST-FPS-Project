using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace Player.Components
{
    public class PlayerMovement 
    {
        public static void ProcessMove(Transform _player, Vector2 input, CharacterController controller)
        {
            Vector3 moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;
            controller.Move(_player.TransformDirection(moveDirection) * PlayerController.playerSpeed * Time.deltaTime);

            //ProcessGravity();
        }
    }
}

