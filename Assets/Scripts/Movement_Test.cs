using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Speedmarket
{
    public class Movement_Test : MonoBehaviour
    {
        PlayerController _controller = new PlayerController();

        void Update()
        {
            _controller.OldInput = _controller.NewInput;
            _controller.NewInput = new PlayerInput();
            
            if(Input.GetKey(KeyCode.W))
            {
                _controller.NewInput.W = true;
            }
            if(Input.GetKey(KeyCode.S))
            {
                _controller.NewInput.S = true;
            }
            if(Input.GetKey(KeyCode.A))
            {
                _controller.NewInput.A = true;
            }
            if(Input.GetKey(KeyCode.D))
            {
                _controller.NewInput.D = true;
            }

            if(Input.GetKey(KeyCode.LeftShift))
            {
                _controller.NewInput.LShift = true;
            }

            PlayerInput.ProcessKeyboard(this, _controller);
        }
    }
}
