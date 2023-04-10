using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace character.stat
{
    [Serializable]
    public class StatComponent
    {
        public StatComponent(List<Stat> characterStats)
        {
            this.characterStats = new List<Stat>();
            if (characterStats != null)
            {
                characterStats.ForEach(s =>
                {
                    this.characterStats.Add(new Stat(s.StatTypes, s.Amount, s.MaxAmount));
                });

            }
        }

        /// <summary>
        /// A stat container for each character stats 
        /// </summary>
        public List<Stat> characterStats;

        /// <summary>
        /// This method allows to give bonus or minus to a stat value
        /// </summary>
        /// <param name="statTypes"> the type of the stat</param>
        /// <param name="amount">Amount to ass or remove </param>
        public virtual void AddOrRemoveStat(E_Stats statTypes, float amount)
        {
            Stat stat = characterStats.Where(s => s.StatTypes == statTypes).FirstOrDefault();
            stat.AddOrRemove(amount);
        }

        /// <summary>
        /// This method allows to set the stat of the character
        /// </summary>
        /// <param name="statTypes"> the type of the stat</param>
        /// <param name="amount">Amount to ass or remove </param>
        public virtual void SetStatAtValue(E_Stats statTypes, float amount)
        {
            Stat stat = characterStats.Where(s => s.StatTypes == statTypes).FirstOrDefault();
            stat.SetValue(amount);
        }

        /// <summary>
        /// Returns the value of a character stat
        /// </summary>
        /// <param name="statTypes"></param>
        /// <returns></returns>
        public virtual float GetStatValue(E_Stats statTypes)
        {
            return characterStats.Where(s => s.StatTypes == statTypes).FirstOrDefault().Amount;
        }
    }
}

