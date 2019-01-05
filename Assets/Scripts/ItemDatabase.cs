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
        public string Name;
        public int Value;
        public Sprite Sprite;
    }

    public enum Category
    {
        Invalid,
        General,
        Food,
        Fashion,
        Sportswear,
        Footwear,
        Perfumes,
        Pharmaceuticals,
    }

    public class ItemDatabase : MonoBehaviour
    {
        [SerializeField] Item[] _invalid;
        [SerializeField] Item[] _general;
        [SerializeField] Item[] _food;
        [SerializeField] Item[] _fashion;
        [SerializeField] Item[] _sportswear;
        [SerializeField] Item[] _footwear;
        [SerializeField] Item[] _perfumes;
        [SerializeField] Item[] _pharmaceuticals;

        public Item[][] GetDatabase()
        {
            Item[][] db =
            {
                _invalid,
                _general,
                _food,
                _fashion,
                _sportswear,
                _footwear,
                _perfumes,
                _pharmaceuticals,
            };

            return db;
        }
    }
}
