using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Speedmarket
{
    /*
        NOTE(SpectatorQL): Manage state in a seperate manager object?
        TODO: Rework all the state processing and UI updating so that they use callbacks.
    */
    public class PlayerEntity : MonoBehaviour
    {
        PlayerController _controller = new PlayerController();
        [HideInInspector] public int Sprint = 1000;
        [HideInInspector] public int SprintMax = 1000;
        [HideInInspector] public float CooldownSecs = 2.0f;
        [HideInInspector] public float NextSprint = 0.0f;

        int _score;

        UI _ui;

        public void UpdateScore(int val)
        {
            _score += val;
            _ui.UpdateScoreText(_score);
        }

        void Start()
        {
            _ui = FindObjectOfType<UI>();
            _ui.UpdateScoreText(0);
            _ui.UpdateSprintBar(Sprint);
        }

        void Update()
        {
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

            _ui.UpdateSprintBar(Sprint);
        }
    }
}
