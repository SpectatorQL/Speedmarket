using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Speedmarket
{
    public class WorldItem : MonoBehaviour, IComparable<WorldItem>
    {
        [HideInInspector] public int Value;
        public Category Category;
        [HideInInspector] public SpriteRenderer Renderer;

        public Action<int> OnPickup;

        bool _triggered;

        public int CompareTo(WorldItem other)
        {
            int result = 0;
            
            if(Category < other.Category)
            {
                result = -1;
            }
            else if(other.Category < Category)
            {
                result = 1;
            }

            return result;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if(_triggered)
                return;

            OnPickup(Value);

            gameObject.SetActive(false);
            _triggered = true;
        }
    }
}
