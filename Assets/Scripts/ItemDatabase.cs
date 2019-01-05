﻿using System;
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
        Mail,
    }

    public class ItemDatabase : MonoBehaviour
    {
        public Item[] Invalid;
        public Item[] General;
    }
}
