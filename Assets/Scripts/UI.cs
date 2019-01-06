using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Speedmarket
{
    public class UI : MonoBehaviour
    {
        [SerializeField] TMP_Text _scoreText;
        [SerializeField] TMP_Text _timeText;
        [SerializeField] Slider _sprintSlider;

        [SerializeField] GameObject _pauseMenu;

        public void UpdateScoreText(int val)
        {
            _scoreText.text = val.ToString();
        }

        public void UpdateTime(float val)
        {
            string str = FormatTime(val);
            _timeText.text = str;
        }

        public void UpdateSprintBar(int val)
        {
            _sprintSlider.value = val;
        }

        string FormatTime(float time)
        {
            string timeString;

            string mainSeparator = ":";
            string secondsFormat = "0.00";
            if(time >= 60)
            {
                int minutes = (int)time / 60;
                float seconds = time - (60 * minutes);
                timeString = minutes.ToString()
                    + mainSeparator
                    + seconds.ToString(secondsFormat,
                        System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                timeString = time.ToString(secondsFormat,
                    System.Globalization.CultureInfo.InvariantCulture);
            }

            return timeString;
        }

        public void ShowPauseMenu()
        {
            _pauseMenu.SetActive(true);
        }

        public void HidePauseMenu()
        {
            _pauseMenu.SetActive(false);
        }
        
        public void QuitGame()
        {
            Manager.QuitApplication();
        }
    }
}
