using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Speedmarket
{
    public class PlayerController
    {
        public PlayerInput OldInput;
        public PlayerInput NewInput;
    }
    
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PlayerInput
    {
        public float MouseX;
        public float MouseY;
        public bool LmbDown;
        public bool RmbDown;

        public bool WDown;
        public bool SDown;
        public bool ADown;
        public bool DDown;
        // TODO: Switch to a different key.
        public bool FDown;
        public bool FUp;

        public bool PDown;

        public bool Debug_LShift;
        
        // TODO: Rethink the whole approach to input processing.
        public static void ProcessKeyboard(PlayerEntity ent, PlayerController controller)
        {
            if(controller.NewInput.PDown)
            {
                ent.OnPause();
            }

            if(ent.IsPaused())
                return;

            float speed = 50.0f;
            float dtSpeed = speed * Time.deltaTime;
            if(controller.NewInput.Debug_LShift)
            {
                dtSpeed *= 10.0f;
            }

            // TODO: Disable sprinting if the character is not moving during the current frame.
            // TODO: If the player pauses the game while pressing F the cooldown never triggers. Gotta fix this.
            float time = Time.time;
            int sprintModifier = 4;
            float sprintMultiplier = 2.0f;
            if((time - ent.NextSprint) > 0)
            {
                if(controller.NewInput.FUp)
                {
                    ent.NextSprint = time + ent.CooldownSecs;
                }
                else
                {
                    if(controller.NewInput.FDown)
                    {
                        if(ent.Sprint > 0)
                        {
                            dtSpeed *= sprintMultiplier;
                            ent.Sprint -= sprintModifier;
                        }
                        else
                        {
                            ent.NextSprint = time + ent.CooldownSecs;
                        }
                    }
                    else
                    {
                        if(ent.Sprint != ent.SprintMax)
                        {
                            ent.Sprint += sprintModifier;
                        }
                    }

                    ent.OnSprintUpdate(ent.Sprint);
                }
            }
            // Debug.Log("Sprint: " + ent.Sprint);
            
            Vector2 velocity = new Vector2();
            var body = ent.GetComponent<Rigidbody2D>();
            if(controller.NewInput.WDown)
            {
                velocity.y = dtSpeed;
            }
            if(controller.NewInput.SDown)
            {
                velocity.y = -dtSpeed;
            }
            if(controller.NewInput.ADown)
            {
                velocity.x = -dtSpeed;
            }
            if(controller.NewInput.DDown)
            {
                velocity.x = dtSpeed;
            }
            body.velocity = velocity;
        }
    }
}
