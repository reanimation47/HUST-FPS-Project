using System.Collections;
using System.Collections.Generic;
using Player.Components;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private static CharacterController controller;
        private Vector3 playerVelocity;
        private bool isGrounded;

        public static readonly float playerSpeed = 5f;
        public float gravity = -9.8f;
        public float jumpHeight = 1f;

        public static readonly float xSensitivity = 30f;
        public static readonly float ySensitivity = 30f;


        public Camera _cam;


        void Start()
        {
            controller = GetComponent<CharacterController>();
            AssignComponents();
        }

        private void Update()
        {
            isGrounded = controller.isGrounded;
        }

        #region Assign Components
        private void AssignComponents()
        {
            PlayerLookAround.AssignCamera(_cam);
            PlayerLookAround.AssignPlayerTransform(transform);
            PlayerMovement.AssignPlayerTransform(transform);
            PlayerMovement.AssignController(controller);
        }
        #endregion

        #region All Controls
        public void ProcessMove(Vector2 input)
        {
            PlayerMovement.ProcessMove(input);
            ProcessGravity();
        }

        public void ProcessLookAround(Vector2 input)
        {
            PlayerLookAround.ProcessLookAround(input);
        }
        
        public void Jump()
        {
            if (isGrounded)
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
            }
        }
        #endregion

        #region Gravity
        private void ProcessGravity()
        {
            playerVelocity.y += gravity * Time.deltaTime;
            if (isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = -2f;
            }
            controller.Move(playerVelocity * Time.deltaTime);
        }
        #endregion



    }

}

