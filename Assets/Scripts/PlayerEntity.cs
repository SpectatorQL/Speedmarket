using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Speedmarket
{
    public class PlayerEntity : MonoBehaviour
    {
        [HideInInspector] public int Sprint = 1000;
        [HideInInspector] public int SprintMax = 1000;
        [HideInInspector] public float CooldownSecs = 2.0f;
        [HideInInspector] public float NextSprint = 0.0f;

        public Action<int> OnSprintUpdate;
        public Action OnPause;
        public Func<bool> IsPaused;
    }
}
