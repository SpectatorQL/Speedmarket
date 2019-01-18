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

        public bool W;
        public bool S;
        public bool A;
        public bool D;
        public bool LShift;
        public bool LShiftUp;

        public bool PDown;
        
        public static void ProcessKeyboard(PlayerEntity ent, PlayerController controller, bool gamePaused)
        {
            float time = Time.time;

            if(!gamePaused)
            {
                float speed = 50.0f;
                float dtSpeed = speed * Time.deltaTime;

                // TODO: Disable sprinting if the character is not moving during the current frame.
                int sprintModifier = 4;
                float sprintMultiplier = 2.0f;
                if((time - ent.NextSprint) > 0)
                {
                    ent.Sprinting = false;

                    if(controller.NewInput.LShiftUp)
                    {
                        ent.NextSprint = time + ent.CooldownSecs;
                    }
                    else
                    {
                        if(controller.NewInput.LShift)
                        {
                            if(ent.Sprint > 0)
                            {
                                dtSpeed *= sprintMultiplier;
                                ent.Sprint -= sprintModifier;
                                ent.Sprinting = true;
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


                Vector2 velocity = new Vector2();
                var body = ent.GetComponent<Rigidbody2D>();
                if(controller.NewInput.W)
                {
                    velocity.y = dtSpeed;
                }
                if(controller.NewInput.S)
                {
                    velocity.y = -dtSpeed;
                }
                if(controller.NewInput.A)
                {
                    velocity.x = -dtSpeed;
                }
                if(controller.NewInput.D)
                {
                    velocity.x = dtSpeed;
                }
                body.velocity = velocity;


                if(controller.NewInput.PDown)
                {
                    ent.OnPause();
                }
            }
            else
            {
                if(controller.NewInput.PDown)
                {
                    if((time - ent.NextSprint) <= 0)
                    {
                        ent.OnPause();
                    }
                    else
                    {
                        if(!controller.NewInput.LShift
                            && ent.Sprinting)
                        {
                            ent.NextSprint = time + ent.CooldownSecs;
                        }

                        ent.OnPause();
                    }
                }
            }
        }
    }
}
