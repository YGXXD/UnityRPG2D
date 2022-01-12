using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMpSystem : Sing<PlayerMpSystem>
{
    [SerializeField] private MpStateBar mpState;
    [SerializeField] private float mp;
    [SerializeField] private float maxMp = 100;
    protected override void Awake()
    {
        Initialize();
        base.Awake();
    }
    private void Initialize()
    {
        mp = maxMp;
        mpState.UpdateState(this.mp, maxMp);
    }
    public void TakeMp(float mp)
    {
        this.mp -= mp;
        mpState.UpdateState(this.mp, maxMp);
    }
    public bool CanUseMp(float useMp)
    {
        if (mp >= useMp) 
        {
            return true;
        }
        else
        {
            return false;
        }    
    }
}
