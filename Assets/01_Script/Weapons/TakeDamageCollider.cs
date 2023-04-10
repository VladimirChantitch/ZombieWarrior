using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace combat
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class TakeDamageCollider : MonoBehaviour
    {
        [HideInInspector] public TakeDamageEvent onTakeDamage = new TakeDamageEvent();
        [SerializeField] BoxCollider2D collider2D;

        private void Awake()
        {
            if (collider2D == null) GetComponent<Collider2D>();
        }

        public virtual void TakeDamage(DamageData damageData)
        {
            onTakeDamage?.Invoke(damageData);
        }

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

