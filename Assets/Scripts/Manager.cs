using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Speedmarket
{
    public class Manager : MonoBehaviour
    {
        PlayerEntity _player;
        PlayerController _playerController = new PlayerController();

        int _score;

        float _timeLimit = 60.0f;
        float _timeLeft;
        bool _outOfTime;

        bool _paused;

        UI _ui;

        static System.Random _rand = new System.Random();

        public void UpdateScore(int val)
        {
            _score += val;
            _ui.UpdateScoreText(_score);
        }

        public void UpdateSprint(int val)
        {
            _ui.UpdateSprintBar(val);
        }

        public void UpdateTime(float val)
        {
            _ui.UpdateTime(val);
        }

        void OnPause()
        {
            if(_paused)
            {
                _ui.HidePauseMenu();
                _paused = false;
                Time.timeScale = 1;
            }
            else
            {
                _ui.ShowPauseMenu();
                _paused = true;
                Time.timeScale = 0;
            }
        }

        bool IsPaused()
        {
            bool result = _paused;
            return result;
        }

        void Start()
        {
            // TODO: Use separate arrays for each category?
            var s = new System.Diagnostics.Stopwatch();
            s.Start();

            var itemDatabase = FindObjectOfType<ItemDatabase>().Items;
            var objs = FindObjectsOfType<WorldItem>()
                .OrderBy(item => item)
                .GroupBy(item => { return item.Category; });
            foreach(var category in objs)
            {
                Item[] categoryItems = itemDatabase
                    .Where(item => item.Category == category.Key)
                    .ToArray();
                foreach(var worldItem in category)
                {
                    int i = _rand.Next(categoryItems.Length);
                    worldItem.Value = categoryItems[i].Value;
                    worldItem.Renderer = worldItem.gameObject.GetComponent<SpriteRenderer>();
                    worldItem.Renderer.sprite = categoryItems[i].Sprite;
                    worldItem.OnPickup = UpdateScore;
                }
            }

            s.Stop();
            string str = string.Format("{0}ms, {1}ticks", s.ElapsedMilliseconds, s.Elapsed.Ticks);
            Debug.Log(str);


            _player = FindObjectOfType<PlayerEntity>();
            _player.OnSprintUpdate = UpdateSprint;
            _player.OnPause = OnPause;
            _player.IsPaused = IsPaused;


            _ui = FindObjectOfType<UI>();
            _ui.UpdateScoreText(0);
            _ui.UpdateSprintBar(_player.Sprint);

            _timeLeft = _timeLimit;
        }
        
        void Update()
        {
            _playerController.OldInput = _playerController.NewInput;
            _playerController.NewInput = new PlayerInput();

            if(Input.GetKey(KeyCode.W))
            {
                _playerController.NewInput.WDown = true;
            }
            if(Input.GetKey(KeyCode.S))
            {
                _playerController.NewInput.SDown = true;
            }
            if(Input.GetKey(KeyCode.A))
            {
                _playerController.NewInput.ADown = true;
            }
            if(Input.GetKey(KeyCode.D))
            {
                _playerController.NewInput.DDown = true;
            }

            if(Input.GetKey(KeyCode.F))
            {
                _playerController.NewInput.FDown = true;
            }
            if(Input.GetKeyUp(KeyCode.F))
            {
                _playerController.NewInput.FUp = true;
            }

            if(Input.GetKeyDown(KeyCode.P))
            {
                _playerController.NewInput.PDown = true;
            }

            if(Input.GetKey(KeyCode.LeftShift))
            {
                _playerController.NewInput.Debug_LShift = true;
            }

            PlayerInput.ProcessKeyboard(_player, _playerController);


            UpdateTime(_timeLeft);

            _timeLeft -= Time.deltaTime;
            if(_timeLeft <= 0)
            {
                Time.timeScale = 0;
                _outOfTime = true;
                // TODO: End level.
            }
        }
    }
}
