using System.Collections;
using System.Collections.Generic;
using Player.Components;
using UnityEngine;
using TMPro;

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
        public static float interactDistance { get; private set; }
        public static LayerMask interactMask { get; private set; }
        public static TextMeshProUGUI promptMessage { get; set; }

        public static readonly float xSensitivity = 30f;
        public static readonly float ySensitivity = 30f;

        [Header("Player Basic Settings")]
        public float _playerSpeed = 5f;
        public float _crouchSpeed = 2f;
        public float gravity = -9.8f;
        public float jumpHeight = 1f;

        [Header("Player Interaction Settings")]
        public float _interactDistance = 3f;
        public LayerMask _interactMask;
        public TextMeshProUGUI _promptMessage;

        

        

        public Camera _cam;
        #endregion

        void Start()
        {
            AssignStaticVariables();
            AssignComponents();
        }

        private void Update()
        {
            isGrounded = controller.isGrounded;
            PlayerInteract.CheckInteraction();
            PlayerCrouch.ProcessCrouch();
        }

        #region Assign static variables
        private void AssignStaticVariables()
        {
            controller = GetComponent<CharacterController>();
            playerTransform = transform;
            playerSpeed = _playerSpeed;
            interactDistance = _interactDistance;
            interactMask = _interactMask;
            promptMessage = _promptMessage;
        }

        #endregion

        #region Assign Components
        private void AssignComponents()
        {
            PlayerLookAround.AssignCamera(_cam);
            PlayerInteract.AssignCamera(_cam);
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

