using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillSystem : Sing<PlayerSkillSystem>
{
    public SkillData[] Skills => skills;
    [SerializeField] private Transform poolParent;
    [SerializeField] private SkillData[] skills;
    private Dictionary<int, SkillData> skillDictionary;
    private SkillData tempSkill;//用于储存获取连击技能
    private SkillData skill;//用于获取非连击技能
    private Coroutine waitDoubleHit;
    protected override void Awake()
    {
        skillDictionary = new Dictionary<int, SkillData>();
        base.Awake();
        InitializeSkill();
        InitializePoll();
    }
    private void InitializeSkill()
    {
        foreach (SkillData item in skills)
        {
#if UNITY_EDITOR
            if (skillDictionary.ContainsKey(item.SkillID))
            {
                Debug.LogError(item.SkillID + " Have Contains key");
            }
#endif
            skillDictionary.Add(item.SkillID, item);
        }
    }
    private void InitializePoll()
    {
        foreach (SkillData item in skills)
        {
            if (item.SkillObject != null)
            {
#if UNITY_EDITOR

                if (PoolManager.dictionary.ContainsKey(item.SkillObject))
                {
                    Debug.LogError("The Prefab Contains:" + item.SkillObject.name);
                    continue;
                }
#endif
                Pool skillObject = new Pool(item.SkillObject);
                PoolManager.dictionary.Add(item.SkillObject, skillObject);
                GameObject sPool = new GameObject("Pool " + item.SkillObject.name + ":");
                sPool.transform.parent = poolParent;
                skillObject.Initialize(sPool.transform);
            }
        }

        foreach(SkillData item in skills)
        {
            if (item.SkillObject != null)
            {
#if UNITY_EDITOR
                if (PoolManager.dictionary.ContainsKey(item.SkillVFX))
                {
                    Debug.LogError("The Prefab Contains:" + item.SkillVFX.name);
                    continue;
                }

#endif
                Pool VFXObject = new Pool(item.SkillVFX);
                PoolManager.dictionary.Add(item.SkillVFX, VFXObject);
                GameObject VFXPool = new GameObject("Pool " + item.SkillVFX.name + ":");
                VFXPool.transform.parent = poolParent;
                VFXObject.Initialize(VFXPool.transform);
            }
        }
    }
    public SkillData GetSkill(int id)
    {
        if (!skillDictionary.ContainsKey(id))
        {
            Debug.LogError("Not Find " + id);
            return null;
        }
        return skillDictionary[id];
    }
    public void ReleaseSkill(int id,bool doubleHit = false)
    {
        if (doubleHit)
        {
            int tempID = id;
            if (tempSkill != null)
                id = tempSkill.NextSkillID;
            tempSkill = GetSkill(id);
            if (tempSkill == null)
            {
                Debug.Log("没有学习该技能");
                return;
            }
            if (tempSkill.IsUsable && PlayerMpSystem.Instance.CanUseMp(tempSkill.MpUse))
            {
                PlayerMpSystem.Instance.TakeMp(tempSkill.MpUse);
                PoolManager.Realease(tempSkill.SkillObject, tempSkill.ReleaseTransform.position,
                    tempSkill.ReleaseTransform.rotation, Vector3.one);
                if (waitDoubleHit != null)
                    StopCoroutine(waitDoubleHit);
                if (tempSkill.NextSkillID == 0)
                    CoolTimeDown(tempID, true);
                else
                    waitDoubleHit = StartCoroutine(WaitDoubleHitCoroutine(tempID));
            }
            else
            {
                tempSkill = null;
                Debug.Log("技能不可用");
            }
        }
        else
        {
            skill = GetSkill(id);
            if (skill == null)
            {
                Debug.Log("没有学习该技能");
                return;
            }
            if (skill.IsUsable && PlayerMpSystem.Instance.CanUseMp(skill.MpUse))
            {
                PlayerMpSystem.Instance.TakeMp(skill.MpUse);
                PoolManager.Realease(skill.SkillObject, skill.ReleaseTransform.position,
                    skill.ReleaseTransform.rotation, Vector3.one);
                CoolTimeDown(id);
            }
            else
            {
                skill = null;
                Debug.Log("技能不可用");
            }
        }
    }

    private IEnumerator WaitDoubleHitCoroutine(int id)
    {
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime;
            yield return null;
        }
        CoolTimeDown(id,true);
    }
    private void CoolTimeDown(int id,bool doubleHit = false)
    {
        StartCoroutine(GetSkill(id).CDEnterCoroutine());
        if (doubleHit)
            tempSkill = null;
        else
            skill = null;
    }
    public void LearnNewSkill(SkillData skillData)
    {
        //将技能加入对象池
        Pool newSkill = new Pool(skillData.SkillObject);
        PoolManager.dictionary.Add(skillData.SkillObject,newSkill);
        GameObject newSkillPool = new GameObject("Pool " + skillData.SkillObject.name + ":");
        newSkillPool.transform.parent = poolParent;
        newSkill.Initialize(newSkillPool.transform);
        //将技能特效加入对象池
        Pool newSkillVFX = new Pool(skillData.SkillVFX);
        PoolManager.dictionary.Add(skillData.SkillVFX,newSkillVFX);
        GameObject newVFXPool = new GameObject("Pool " + skillData.SkillVFX.name + ":");
        newVFXPool.transform.parent = poolParent;
        newSkillVFX.Initialize(newVFXPool.transform);
        //将技能加入技能字典
        skillDictionary.Add(skillData.SkillID, skillData);
    }

    public void ChangeSkill(int skillID)
    {
        int tempSkillID = PlayerInput.SkillID;
        PlayerInput.SkillID = skillID;
        GetSkill(tempSkillID).SkillUI.SetActive(false);
        GetSkill(skillID).SkillUI.SetActive(true);
    }
}