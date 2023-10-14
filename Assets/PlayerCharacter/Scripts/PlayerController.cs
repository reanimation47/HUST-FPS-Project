using System.Collections;
using System.Collections.Generic;
using Player.Components;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Initialize Variables
        public static CharacterController controller;
        public static Transform playerTransform;
        private Vector3 playerVelocity;
        private bool isGrounded;

        public static float playerSpeed { get; private set; }

        public float _playerSpeed = 5f;
        public float _crouchSpeed = 2f;
        public float gravity = -9.8f;
        public float jumpHeight = 1f;

        public static readonly float xSensitivity = 30f;
        public static readonly float ySensitivity = 30f;

        

        public Camera _cam;
        #endregion

        void Start()
        {
            controller = GetComponent<CharacterController>();
            playerTransform = transform;
            playerSpeed = _playerSpeed;
            AssignComponents();
        }

        private void Update()
        {
            isGrounded = controller.isGrounded;
            PlayerCrouch.ProcessCrouch();
        }

        #region Assign Components
        private void AssignComponents()
        {
            PlayerLookAround.AssignCamera(_cam);
        }
        #endregion

        #region All Controls Processes
        public void ProcessMove(Vector2 input)
        {
            PlayerMovement.ProcessMove(input);
            ProcessGravity();
        }

        public void ProcessLookAround(Vector2 input)
        {
            PlayerLookAround.ProcessLookAround(input);
        }
        #endregion

        #region Binded actions

        public void Crouch()
        {
            playerSpeed = PlayerCrouch.Crouch() ? _crouchSpeed : _playerSpeed;
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

