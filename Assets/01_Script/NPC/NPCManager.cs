using character.ai;
using character.stat;
using combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace character
{
    public class NPCManager : AbstractCharacterManager
    {
        [SerializeField] NPCStats nPCStats;

        [SerializeField] ZombiTakeDamageCollider zombiTakeDamageCollider;
        [SerializeField] AnimatorManager animator;
        [SerializeField] NPCBrain brain;

        [SerializeField] StatSystem statSystem;
        [SerializeField] SpriteRenderer spriteRenderer;

        public event Action onNpcDied;
        bool isDead;

        private void Awake()
        {
            zombiTakeDamageCollider.onTakeDamage.AddListener(data => { TakeDamage(data); });
            brain.onAnimationPlayed.AddListener((s, b, a) => HandleAnimationEvent(s, b, a));
            statSystem = new StatSystem(nPCStats.characterStats);
        }

        protected override void TakeDamage(DamageData damage)
        {
            if (isDead) return;

            statSystem.AddOrRemoveStat(StatTypes.Life, damage.DamageAmount);

            if(statSystem.GetStatValue(StatTypes.Life) <= 0)
            {
                isDead = true;
                brain.HandleState(AI_States.Dying);
                onNpcDied?.Invoke();
                zombiTakeDamageCollider.CloseCollider();
                spriteRenderer.sortingOrder = -1;
            }
        }

        private void HandleAnimationEvent(string s, bool b, Action a)
        {
            animator.PlayTargetAnimation(s, a);
        }
    }
}

