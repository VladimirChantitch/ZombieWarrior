using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace stats
{
    [CreateAssetMenu(menuName = "Stats/Character")]
    public class StatSystem : ScriptableObject
    {
        public List<Stat> stats = new List<Stat>()
        {
            new Stat(E_Stats.Life, 100, 100),
            new Stat(E_Stats.Stamina, 100, 100),
        };
    }
}

