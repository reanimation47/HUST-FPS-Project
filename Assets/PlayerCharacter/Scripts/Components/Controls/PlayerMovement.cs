using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace Player.Components
{
    public class PlayerMovement 
    {
        public static void ProcessMove(Vector2 input)
        {
            //PlayerBodyAnimation.ProcessMovementAnimation(input);

            Vector3 moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;
            PlayerController.controller.Move(PlayerController.playerTransform.TransformDirection(moveDirection) * PlayerController.playerSpeed * Time.deltaTime);
            
        }
        
    }
}

