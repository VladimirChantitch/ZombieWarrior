using camera;
using character.ai;
using character.stat;
using combat;
using savesystem.realm;
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
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }

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
            HitPostEffet();

            if (statComponent.GetStatValue(E_Stats.Life) <= 0)
            {
                isDead = true;
                brain.HandleState(AI_States.Dying);
                onNpcDied?.Invoke();
                PlayerCrud.Instance.IncreaseHighScore(SeesionCookie.currentPlayerName, (int)statComponent.GetStatValue(E_Stats.value));
                zombiTakeDamageCollider.CloseCollider();
                spriteRenderer.sortingOrder = -1;
            }
        }


        protected void HitPostEffet()
        {
            StartCoroutine(flashSprite());
        }

        IEnumerator flashSprite()
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
        }
    }
}

