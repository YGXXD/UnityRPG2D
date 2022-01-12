using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProp : MonoBehaviour
{
    [SerializeField] private GameObject SkillUI;
    [SerializeField] private SkillData thisSkill;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            PlayerSkillSystem.Instance.LearnNewSkill(thisSkill);
            SkillUI.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
