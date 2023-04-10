using character;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace combat
{
    public class BulletCollider : AbstractInflictDamageCollider
    {
        [HideInInspector] public UnityEvent<BulletCollider> onTimeOut = new UnityEvent<BulletCollider>();
        [SerializeField] GameObject destructionParticles;

        public void SetDamageData(DamageData damageData)
        {
            this.damage = damageData;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == targetLayer)
            {
                TakeDamageCollider takeDamageCollider = collision.GetComponent<TakeDamageCollider>();
                Instantiate(destructionParticles, transform.position, transform.rotation);
                InflictDamage(takeDamageCollider);
            }
            else if (collision.gameObject.layer == 31)
            {
                StopAllCoroutines();
                Instantiate(destructionParticles, transform.position, transform.rotation);
                onTimeOut?.Invoke(this);
            }
        }

        public void OpenCollider(float timer, DamageData damage)
        {
            base.OpenCollider();

            this.damage = damage;
            StartCoroutine(BulletAutoDestruction(timer));
        }

        IEnumerator BulletAutoDestruction(float timer)
        {
            yield return new WaitForSeconds(timer);
            onTimeOut?.Invoke(this);
        }
    }
}

