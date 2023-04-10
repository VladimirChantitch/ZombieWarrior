using combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace character.ai
{
    public class BasicNpc_AttackingState : AbstractActivity
    {
        [SerializeField] List<CloseCombatDamageCollider> colliders = new List<CloseCombatDamageCollider>();
        [SerializeField] int damageAmount;
        bool isActive = true;
        bool alreadyEnded = true;

        private void Start()
        {
            colliders.ForEach(c =>
            {
                c.onInflictDamage += (collider, data, target) =>
                {
                    target.TakeDamage(data);

                    colliders.ForEach(c =>
                    {
                        c.CloseCollider();
                    });
                };

                c.SetDamageData(new DamageData(damageAmount));
            });
        }

        protected override void InitState()
        {
            base.InitState();
            isActive = true;
            onAnimationPlayed?.Invoke(ResourcesManager.ZB_ATTACK_ANIMATION, false, () => EndAttack());
            colliders.ForEach(c =>
            {
                c.OpenCollider();
            });
        }

        protected override void DoStateLogique()
        {

        }

        protected override AI_States GetNextState()
        {
            return AI_States.ChasingTarget;
        }

        protected override bool IsStillActive(bool isIt = true)
        {
            return isActive;
        }

        private void EndAttack()
        {
            colliders.ForEach(c =>
            {
                c.CloseCollider();
            });
            isActive = false;
        }
    }
}

