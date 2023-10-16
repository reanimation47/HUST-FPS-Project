using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Components
{
    public class PlayerCrouch
    {
        private static bool crouching;
        private static float crouchTimer;
        private static bool lerpCrouch;

        public static bool Crouch()
        {
            crouching = !crouching;
            crouchTimer = 0;
            lerpCrouch = true;
            return crouching;
        }

        public static void ProcessCrouch()
        {
            if(lerpCrouch)
            {
                crouchTimer += Time.deltaTime;
                float p = crouchTimer / 1;
                p *= p;
                if (crouching)
                {
                    PlayerController.controller.height = Mathf.Lerp(PlayerController.controller.height, 1, p);
                }else
                {
                    PlayerController.controller.height = Mathf.Lerp(PlayerController.controller.height, 2, p);
                }

                if (p>1)
                {
                    lerpCrouch = false;
                    crouchTimer = 0f;
                }
            }
        }
    }
}

