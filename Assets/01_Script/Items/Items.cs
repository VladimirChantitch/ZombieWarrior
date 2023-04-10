using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace items
{
    public abstract class Items : ScriptableObject
    {
        [SerializeField] string name;
        [SerializeField, TextArea] string description;
        [SerializeField] Rarity rarity;
        [SerializeField] Sprite sprite;

        public string Name { get => name; }
        public string Description { get => description; }
        public Rarity Rarity { get => rarity; }
        public Sprite Sprite { get => sprite; }
    }

    public enum Rarity
    {
        common,
        rare,
        epic,
        legendary
    }
}

