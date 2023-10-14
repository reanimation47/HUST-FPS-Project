using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace Player.Components
{
    public class PlayerMovement 
    {
        private static CharacterController controller;
        private static Transform _player;

        public static void AssignPlayerTransform(Transform _transform)
        {
            _player = _transform;
        }

        public static void AssignController(CharacterController _con)
        {
            controller = _con;
        }

        public static void ProcessMove(Vector2 input)
        {
            Vector3 moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;
            controller.Move(_player.TransformDirection(moveDirection) * PlayerController.playerSpeed * Time.deltaTime);

            //ProcessGravity();
        }
    }
}

