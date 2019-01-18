using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        public void OnPause()
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

        public void QuitToMainMenu()
        {
            SceneManager.LoadSceneAsync(0);
        }

        public static void QuitApplication()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        void Start()
        {
            Item[][] itemDatabase = FindObjectOfType<ItemDatabase>()
                .GetDatabase();

            var spawnPoints = FindObjectsOfType<WorldItem>()
                .OrderBy(item => item)
                .GroupBy(item => { return item.Category; });
            
            foreach(var category in spawnPoints)
            {
                int catIdx = (int)category.Key;
                Item[] categoryItems = itemDatabase[catIdx];

                if(categoryItems.Length != 0)
                {
                    foreach(var worldItem in category)
                    {
                        int j = _rand.Next(categoryItems.Length);
                        worldItem.Value = categoryItems[j].Value;
                        worldItem.Renderer = worldItem.gameObject
                            .GetComponent<SpriteRenderer>();
                        worldItem.Renderer.sprite = categoryItems[j].Sprite;
                        worldItem.OnPickup = UpdateScore;
                    }
                }
                else
                {
                    Item invalidItem = itemDatabase[0][0];
                    foreach(var worldItem in category)
                    {
                        worldItem.Value = invalidItem.Value;
                        worldItem.Renderer = worldItem.gameObject
                            .GetComponent<SpriteRenderer>();
                        worldItem.Renderer.sprite = invalidItem.Sprite;
                        worldItem.OnPickup = UpdateScore;
                    }
                }
            }


            _player = FindObjectOfType<PlayerEntity>();
            _player.OnSprintUpdate = UpdateSprint;
            _player.OnPause = OnPause;


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
                _playerController.NewInput.W = true;
            }
            if(Input.GetKey(KeyCode.S))
            {
                _playerController.NewInput.S = true;
            }
            if(Input.GetKey(KeyCode.A))
            {
                _playerController.NewInput.A = true;
            }
            if(Input.GetKey(KeyCode.D))
            {
                _playerController.NewInput.D = true;
            }

            if(Input.GetKey(KeyCode.LeftShift))
            {
                _playerController.NewInput.LShift = true;
            }
            if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                _playerController.NewInput.LShiftUp = true;
            }

            if(Input.GetKeyDown(KeyCode.P))
            {
                _playerController.NewInput.PDown = true;
            }

            PlayerInput.ProcessKeyboard(_player, _playerController, _paused);


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
