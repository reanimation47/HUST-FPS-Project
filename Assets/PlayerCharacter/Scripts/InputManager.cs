using System.Collections;
using System.Collections.Generic;
using Player.Components;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InputManager : MonoBehaviour
    {
        private PlayerInput playerInput;
        private PlayerInput.OnFootActions onFoot;

        private PlayerController controller;

        public WeaponHandler.BaseGunController gunController;

        private void Awake()
        {
            playerInput = new PlayerInput();
            onFoot = playerInput.OnFoot;
            controller = GetComponent<PlayerController>();
            onFoot.Jump.performed += ctx => controller.Jump();
            onFoot.Crouch.performed += ctx => controller.Crouch();
        }

        private void Update()
        {
            if (playerInput.OnFoot.Interact.triggered)
            {
                PlayerInteract.Interact();
            }
            if (playerInput.OnFoot.LeftClick.IsPressed())
            {
                gunController.ShootGun();
            }

            if (playerInput.OnFoot.RightClick.IsPressed())
            {
                gunController.DetermineAim(true);
            }
            else
            {
                gunController.DetermineAim(false);
            }

            if (playerInput.OnFoot.Reload.triggered)
            {
                gunController.Reload();
            }
        }

        private void FixedUpdate()
        {
            controller.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        }

        private void LateUpdate()
        {
            controller.ProcessLookAround(onFoot.Look.ReadValue<Vector2>());
            gunController.HandleRotation(onFoot.Look.ReadValue<Vector2>());
        }

        private void OnEnable()
        {
            onFoot.Enable();
        }

        private void OnDisable()
        {
            onFoot.Disable();
        }
    }
}

