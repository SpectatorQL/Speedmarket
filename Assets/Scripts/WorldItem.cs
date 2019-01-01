using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Speedmarket
{
    public class WorldItem : MonoBehaviour, IComparable<WorldItem>
    {
        public Category Category;
        [HideInInspector] public SpriteRenderer Renderer;

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

        void Start()
        {
            Renderer = GetComponent<SpriteRenderer>();
        }
    }
}
