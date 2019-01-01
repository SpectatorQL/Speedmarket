using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Speedmarket
{
    [Serializable]
    public class Item
    {
        public int Value;
        public Sprite Sprite;
        public Category Category;
    }

    public enum Category
    {
        Invalid = -1,
        General,
        Food,
        Fashion,
        Sportswear,
        Footwear,
        Perfumes,
        Pharmaceuticals,
        Mail,
    }

    public class ItemDatabase : MonoBehaviour
    {
        public Item[] Items;
        static System.Random _rand = new System.Random();

        void Start()
        {
            var objs = FindObjectsOfType<WorldItem>()
                .OrderBy(item => item)
                .GroupBy(item => { return item.Category; });
            foreach(var category in objs)
            {
                Item[] categoryItems = Items
                    .Where(item => item.Category == category.Key)
                    .ToArray();
                foreach(var worldItem in category)
                {
                    int i = _rand.Next(categoryItems.Length);
                    worldItem.Renderer.sprite = categoryItems[i].Sprite;
                }
            }
        }
    }
}
