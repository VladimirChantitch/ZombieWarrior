using character.stat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System;

namespace character.ai
{
    public class AIStateEvent : UnityEvent<AI_States> { }

    public class AIAnimationEvent : UnityEvent<string, bool, Action> { }

    public abstract class AbstractActivity : MonoBehaviour
    {
        public AI_States aI_States;
        [HideInInspector] public AIStateEvent onStateChange = new AIStateEvent();
        public AIAnimationEvent onAnimationPlayed = new AIAnimationEvent();
        float tick;
        public virtual IEnumerator RunActivity()
        {
            InitState();

            while (IsStillActive())
            {
                yield return new WaitForSeconds(tick);
                DoStateLogique();
            }

            onStateChange?.Invoke(GetNextState());
            yield return null;
        }

        protected abstract AI_States GetNextState();
        protected virtual void InitState()
        {
            tick = GameManager.Instance.Tick;
        }
        protected abstract bool IsStillActive(bool isIt = true);
        protected abstract void DoStateLogique();
    }

    public enum AI_States
    {
        Appearing,
        Idle,
        LookingForTarget,
        ChasingTarget,
        Attacking,
        Dying,
        None
    }
}

