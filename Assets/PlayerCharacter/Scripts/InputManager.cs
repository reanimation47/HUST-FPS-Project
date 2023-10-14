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

        private void Awake()
        {
            playerInput = new PlayerInput();
            onFoot = playerInput.OnFoot;
            controller = GetComponent<PlayerController>();
            onFoot.Jump.performed += ctx => controller.Jump();
        }

        private void FixedUpdate()
        {
            controller.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        }

        private void LateUpdate()
        {
            controller.ProcessLookAround(onFoot.Look.ReadValue<Vector2>());
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

