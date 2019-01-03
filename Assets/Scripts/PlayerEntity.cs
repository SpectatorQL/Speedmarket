using System;
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
        [HideInInspector] public int Sprint = 1000;
        [HideInInspector] public int SprintMax = 1000;
        [HideInInspector] public float CooldownSecs = 2.0f;
        [HideInInspector] public float NextSprint = 0.0f;

        public Action<int> OnSprintUpdate;
    }
}
