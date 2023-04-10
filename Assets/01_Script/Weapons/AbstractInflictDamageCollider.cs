using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace combat
{
    public abstract class AbstractInflictDamageCollider : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D rb;
        [SerializeField] protected Collider2D collider2D;

        [SerializeField] protected int targetLayer;

        public void SetDamageData(DamageData damageData)
        {
            this.damage = damageData;
        }

        public DamageData damage { get; protected set; }
        public Rigidbody2D Rb { get => rb; }

        [HideInInspector] public InflictDamageEvent onInflictDamage = new InflictDamageEvent();

        public virtual void OpenCollider()
        {
            collider2D.enabled = true;
        }

        public virtual void CloseCollider()
        {
            collider2D.enabled = false;
        }
    }
}
