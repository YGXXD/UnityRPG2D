using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillData
{
    public int SkillID
    {
        get => skillID;
        set => skillID = value;
    }
    public float MpUse => mpUse;
    public float SkillDuration => skillDuration;
    public float SkillVFXInterval => skillVFXInterval;
    public float SkillSpeed => skillSpeed;
    public float SkillDamage => skillDamage;
    public GameObject SkillObject => skillObject;
    public float SkillCD => skillCD;
    public GameObject SkillVFX => skillVFX;
    public Transform ReleaseTransform => releaseTransform;
    public float RemainCD => remainCD;
    public bool IsUsable => isUsable;
    public GameObject SkillUI => skillUI;
    public int NextSkillID => nextSkillID;
    [SerializeField] private int skillID;//技能ID
    [SerializeField] private float skillSpeed;//技能速度
    [SerializeField] private float skillDuration;//技能持续时间
    [SerializeField] private float skillDamage;//技能伤害
    [SerializeField] private float skillCD;//技能CD
    [SerializeField] private float mpUse;//技能消耗Mp
    [SerializeField] private GameObject skillObject;//技能预制体
    [SerializeField] private GameObject skillVFX;//技能特效
    [SerializeField] private float skillVFXInterval;//技能间隔
    [SerializeField] private Transform releaseTransform;//技能释放点
    [SerializeField] private GameObject skillUI;//技能UI
    [SerializeField] private int nextSkillID;
    private float remainCD;
    private bool isUsable = true;
    
    public IEnumerator CDEnterCoroutine()
    {
        UISkillState state = SkillUI.GetComponent<UISkillState>();
        isUsable = false;
        remainCD = skillCD;
        while (remainCD > 0) 
        {
            remainCD -= Time.deltaTime;
            state.UpdateState(remainCD, skillCD);
            yield return null;
        }
        remainCD = 0;
        isUsable = true;
    }


}
