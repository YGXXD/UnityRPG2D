using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackKind : MonoBehaviour
{
    [SerializeField] protected int skillID;
    private SkillData skillData;
    protected virtual void Start()
    {
        skillData = PlayerSkillSystem.Instance.GetSkill(skillID);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyHpSystem>(out EnemyHpSystem enemy))
        {
            enemy.TakeDamage(skillData.SkillDamage);
            PlayerEnergySystem.Instance.RestoreEnergy(10f);
        }
        PoolManager.Realease(skillData.SkillVFX, collision.GetContact(0).point);
        if(this.gameObject.CompareTag("Projectile"))
            this.gameObject.SetActive(false);
    }
}
