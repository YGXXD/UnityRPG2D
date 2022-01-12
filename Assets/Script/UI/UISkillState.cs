using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkillState : StateBar
{
    protected override void Awake()
    {
        base.Awake();
        UpdateState(0f, 1f);//使技能CD状态UI初始化为0填充(技能可以用)
    }
}
