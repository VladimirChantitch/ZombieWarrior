using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace combat
{
    public class CloseCombatDamageCollider : AbstractInflictDamageCollider
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == targetLayer)
            {
                TakeDamageCollider takeDamageCollider = collision.GetComponent<TakeDamageCollider>();
                onInflictDamage?.Invoke(this, damage, takeDamageCollider);
            }
        }
    }
}

