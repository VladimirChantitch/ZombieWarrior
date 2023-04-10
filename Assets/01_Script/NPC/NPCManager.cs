using character.ai;
using character.stat;
using combat;
using stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace character
{
    public class NPCManager : MonoBehaviour, ICharacters, IDamageable
    {
        [SerializeField] ZombiTakeDamageCollider zombiTakeDamageCollider;
        [SerializeField] AnimationHelper animator;
        [SerializeField] NPCBrain brain;

        [SerializeField] StatComponent statComponent;
        [SerializeField] StatSystem statSystem;
        [SerializeField] SpriteRenderer spriteRenderer;

        public event Action onNpcDied;
        bool isDead;

        [SerializeField] string _name;
        [SerializeField] string _description;

        IDamageable iDamageable = null; 

        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }

        private void Awake()
        {
            iDamageable = (this as IDamageable);
            zombiTakeDamageCollider.onTakeDamage += data => { iDamageable.TakeDamage(data); };
            brain.onAnimationPlayed.AddListener((s, b, a) => HandleAnimationEvent(s, b, a));
            statComponent = new StatComponent(statSystem.stats);
        }

        private void HandleAnimationEvent(string s, bool b, Action a)
        {
            animator.PlayTargetAnimation(s, a);
        }

        void IDamageable.TakeDamage(DamageData damageData)
        {
            if (isDead) return;

            statComponent.AddOrRemoveStat(E_Stats.Life, damageData.DamageAmount);

            if (statComponent.GetStatValue(E_Stats.Life) <= 0)
            {
                isDead = true;
                brain.HandleState(AI_States.Dying);
                onNpcDied?.Invoke();
                zombiTakeDamageCollider.CloseCollider();
                spriteRenderer.sortingOrder = -1;
            }
        }
    }
}

