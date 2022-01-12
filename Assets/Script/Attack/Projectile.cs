using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : AttackKind
{
    protected void OnEnable()
    {
        if (this.transform.childCount != 0)
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                this.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        StartCoroutine(ProjectileMoveCoroutine());
    }
    private IEnumerator ProjectileMoveCoroutine()
    {
        while(this.gameObject.activeSelf)
        {
            this.transform.Translate(Vector2.left * Time.deltaTime * PlayerSkillSystem.Instance.GetSkill(skillID).SkillSpeed);
            yield return null;
        }
    }
}
