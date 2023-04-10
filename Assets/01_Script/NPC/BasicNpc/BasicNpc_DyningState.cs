using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace character.ai
{
    public class BasicNpc_DyningState : AbstractActivity
    {
        public bool once = true;
        bool active = true;

        protected override void DoStateLogique()
        {
            onAnimationPlayed?.Invoke(StringManager.ZB_DEATH_ANIMATION, false, () => active = false);
        }
        protected override AI_States GetNextState()
        {
            return AI_States.None;
        }
        protected override bool IsStillActive(bool isIt = true)
        {
            return active;
        }
    }
}
