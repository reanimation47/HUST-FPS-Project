using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player.Components
{ 
    public class PlayerLookAround
    {
        private static float xRotation = 0f;

        public static Camera _camera;
        private static Transform _player;

        public static void AssignPlayerTransform(Transform _transform)
        {
            _player = _transform;
        }

        public static void AssignCamera(Camera cam)
        {
            _camera = cam;
        }

        public static void ProcessLookAround(Vector2 input)
        {
            float mouseX = input.x;
            float mouseY = input.y;

            xRotation -= (mouseY * Time.deltaTime) * PlayerController.ySensitivity;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);

            _camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

            _player.Rotate(Vector3.up * (mouseX * Time.deltaTime) * PlayerController.xSensitivity);
        }
    }
}

