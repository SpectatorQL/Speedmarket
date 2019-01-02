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
        [SerializeField] Slider _sprintSlider;

        public void UpdateScoreText(int val)
        {
            _scoreText.text = val.ToString();
        }

        public void UpdateSprintBar(int val)
        {
            _sprintSlider.value = val;
        }
    }
}
