using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergySystem : Sing<PlayerEnergySystem>
{
    [SerializeField] private float energy;
    [SerializeField] private float maxEnergy;
    [SerializeField] private EnergyStateBar energyStateBar;
    protected override void Awake()
    {
        Initialize();
        base.Awake();
    }
    private void Initialize()
    {
        energy = 0;
        energyStateBar.UpdateState(energy,maxEnergy);
    }

    public void RestoreEnergy(float restoreEnergy)
    {
        energy += restoreEnergy;
        energy = Mathf.Clamp(energy, 0, maxEnergy);
        energyStateBar.UpdateState(energy,maxEnergy);
    }

    public void TakeEnergy(float useEnergy)
    {
        this.energy -= useEnergy;
        energyStateBar.UpdateState(energy,maxEnergy);
    }

    public bool CanUseEnergy(float useEnergy)
    {
        if (this.energy >= useEnergy) 
        {
            return true;
        }
        else
        {
            return false;
        } 
    }
}
