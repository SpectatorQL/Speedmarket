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
        
        public static void ProcessKeyboard(MonoBehaviour ent, PlayerController controller)
        {
            float speed = 1.0f;
            float dtSpeed = speed * Time.deltaTime;
            if(controller.NewInput.LShift)
            {
                dtSpeed *= 10.0f;
            }

            Vector3 pos = ent.transform.position;
            if(controller.NewInput.W)
            {
                pos += ent.transform.up * dtSpeed;
            }
            if(controller.NewInput.S)
            {
                pos -= ent.transform.up * dtSpeed;
            }
            if(controller.NewInput.A)
            {
                pos -= ent.transform.right * dtSpeed;
            }
            if(controller.NewInput.D)
            {
                pos += ent.transform.right * dtSpeed;
            }
            ent.transform.position = pos;
        }

        public static void ProcessMouse(MonoBehaviour ent, PlayerController controller)
        {
        }
    }
}
