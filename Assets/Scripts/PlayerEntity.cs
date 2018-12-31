using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Speedmarket
{
    public class PlayerEntity : MonoBehaviour
    {
        PlayerController _controller = new PlayerController();
        [HideInInspector] public int Sprint = 1000;
        [HideInInspector] public int SprintMax = 1000;
        [HideInInspector] public float CooldownSecs = 2.0f;
        [HideInInspector] public float NextSprint = 0.0f;

        void Update()
        {
            Debug.Log(CooldownSecs);

            _controller.OldInput = _controller.NewInput;
            _controller.NewInput = new PlayerInput();
            
            if(Input.GetKey(KeyCode.W))
            {
                _controller.NewInput.WDown = true;
            }
            if(Input.GetKey(KeyCode.S))
            {
                _controller.NewInput.SDown = true;
            }
            if(Input.GetKey(KeyCode.A))
            {
                _controller.NewInput.ADown = true;
            }
            if(Input.GetKey(KeyCode.D))
            {
                _controller.NewInput.DDown = true;
            }

            if(Input.GetKey(KeyCode.F))
            {
                _controller.NewInput.FDown = true;
            }
            if(Input.GetKeyUp(KeyCode.F))
            {
                _controller.NewInput.FUp = true;
            }

            if(Input.GetKey(KeyCode.LeftShift))
            {
                _controller.NewInput.Debug_LShift = true;
            }

            PlayerInput.ProcessKeyboard(this, _controller);
        }
    }
}
