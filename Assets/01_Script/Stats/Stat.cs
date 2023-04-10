using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    public Stat(E_Stats statTypes, float amount, float maxAmount = 150)
    {
        this.statTypes = statTypes;
        this.amount = amount;
        if (amount > maxAmount)
        {
            this.maxAmount = amount;
        }
        else
        {
            this.maxAmount = maxAmount;
        }
    }

    [SerializeField] private E_Stats statTypes;
    public E_Stats StatTypes { get { return statTypes; } }
    [SerializeField] private float amount;
    public float Amount { get { return amount; } }
    [SerializeField] private float maxAmount;
    public float MaxAmount { get { return maxAmount; } }

    public void AddOrRemove(float amount)
    {
        this.amount += amount;
        CheckClamp();
    }

    public void SetValue(float amount)
    {
        this.amount = amount;
        CheckClamp();
    }

    private void CheckClamp()
    {
        if (amount > maxAmount)
        {
            amount = maxAmount;
        }
    }
}