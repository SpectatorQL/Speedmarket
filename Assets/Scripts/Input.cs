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

        public bool FDown;
        public bool FUp;

        public bool Debug_LShift;
        
        public static void ProcessKeyboard(PlayerEntity ent, PlayerController controller)
        {
            float speed = 1.0f;
            float dtSpeed = speed * Time.deltaTime;
            if(controller.NewInput.Debug_LShift)
            {
                dtSpeed *= 10.0f;
            }

            // TODO: Disable sprinting if the character is not moving during the current frame.
            float time = Time.time;
            int sprintModifier = 4;
            float sprintValue = 3.0f;
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
                            dtSpeed *= sprintValue;
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
                }
            }
            // Debug.Log("Sprint: " + ent.Sprint);
            
            Vector3 pos = ent.transform.position;
            if(controller.NewInput.WDown)
            {
                pos += ent.transform.up * dtSpeed;
            }
            if(controller.NewInput.SDown)
            {
                pos -= ent.transform.up * dtSpeed;
            }
            if(controller.NewInput.ADown)
            {
                pos -= ent.transform.right * dtSpeed;
            }
            if(controller.NewInput.DDown)
            {
                pos += ent.transform.right * dtSpeed;
            }
            ent.transform.position = pos;
        }

        public static void ProcessMouse(PlayerEntity ent, PlayerController controller)
        {
        }
    }
}
