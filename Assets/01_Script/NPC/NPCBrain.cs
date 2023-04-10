using character.stat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.AI;

namespace character.ai
{
    public class NPCBrain : MonoBehaviour
    {
        [SerializeField] List<AbstractActivity> states = new List<AbstractActivity>();
        Coroutine coroutine;
        [SerializeField] AI_States currentState;
        [HideInInspector] public AIAnimationEvent onAnimationPlayed = new AIAnimationEvent();
        [SerializeField] NavMeshAgent navMeshAgent;

        private void Awake()
        {
            states.ForEach(s =>
            {
                s.onStateChange.AddListener(newState => HandleState(newState));
                s.onAnimationPlayed.AddListener((s, b, a) => onAnimationPlayed?.Invoke(s, b, a));
            });
        }

        private void Start()
        {
            navMeshAgent.updateRotation = false;
            navMeshAgent.updateUpAxis = false;

            HandleState(currentState);
        }

        public void HandleState(AI_States aI_States)
        {
            if (aI_States == AI_States.None || currentState == AI_States.None)
            {   
                currentState = AI_States.None;
                return;
            }

            currentState = aI_States;
            navMeshAgent.isStopped = true;

            List<AbstractActivity> possibleState = states.Where(state => state.aI_States == aI_States).ToList();
            if (coroutine == null)
            {
                coroutine = StartCoroutine(possibleState[Random.Range(0, possibleState.Count)].RunActivity());
            }
            else
            {
                StopCoroutine(coroutine);
                coroutine = StartCoroutine(possibleState[Random.Range(0, possibleState.Count)].RunActivity());
            }
        }
    }
}

