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

        public void UpdateScoreDisplay(int val)
        {
            _scoreText.text = val.ToString();
        }
    }
}
