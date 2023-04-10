using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace character.ai
{
    public class BasicNpc_ApperaingState : AbstractActivity
    {
        [SerializeField] AI_States nextState;
        protected override void DoStateLogique()
        {
            //Appearing
        }

        protected override AI_States GetNextState()
        {
            return nextState;
        }

        protected override bool IsStillActive(bool isIt = true)
        {
            return false;
        }
    }
}

