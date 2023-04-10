using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace combat
{
    [Serializable]
    public class DamageData
    {
        public DamageData(float damageAmount)
        {
            this.damageAmount = damageAmount;
        }

        [SerializeField] float damageAmount;

        public float DamageAmount { get => damageAmount; }
    }
}
