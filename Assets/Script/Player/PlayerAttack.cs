using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public bool IsDashing => isDashing;
    [SerializeField] private int dashSkillID;
    private bool isDashing = false;
    private SkillData dashSkill;
    private Rigidbody2D thisRigidbody;

    private void Awake()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
        dashSkill = PlayerSkillSystem.Instance.GetSkill(dashSkillID);
    }
    public void Attack(int id)
    {
        if (!PlayerAnimator.Instance.IsPlaying("Attack"))
        {
            if (PlayerSkillSystem.Instance.GetSkill(id) != null 
                && PlayerSkillSystem.Instance.GetSkill(id).IsUsable 
                && PlayerMpSystem.Instance.CanUseMp(PlayerSkillSystem.Instance.GetSkill(id).MpUse))
            {
                PlayerAnimator.Instance.PlayerPlay("Attack");
            }

            if (id == 2)
                PlayerSkillSystem.Instance.ReleaseSkill(id, true);
            else
                PlayerSkillSystem.Instance.ReleaseSkill(id);
        }

    }
    public void Dash()
    {
        if (dashSkill.IsUsable && PlayerMpSystem.Instance.CanUseMp(dashSkill.MpUse))
        {
            PlayerAnimator.Instance.PlayerPlay("Dash");
            StartCoroutine(DashCoroutine());
            StartCoroutine(DashShaderSpawnCorouting());
        }
    }
    private IEnumerator DashCoroutine()
    {
        isDashing = true;
        this.gameObject.layer = 10;
        PlayerSkillSystem.Instance.ReleaseSkill(dashSkillID);
        float t = 0;
        while (t < dashSkill.SkillDuration)
        {
            t += Time.deltaTime;
            thisRigidbody.velocity = -dashSkill.SkillSpeed * this.transform.right;
            yield return null;
        }
        this.gameObject.layer = 8;
        isDashing = false;
        thisRigidbody.velocity = Vector2.zero;
    }
    private IEnumerator DashShaderSpawnCorouting()
    {
        float t = 0;
        while (t < dashSkill.SkillDuration)
        {
            t += dashSkill.SkillVFXInterval;
            PoolManager.Realease(dashSkill.SkillVFX, dashSkill.ReleaseTransform.position, dashSkill.ReleaseTransform.rotation);
            yield return new WaitForSeconds(dashSkill.SkillVFXInterval);
        }

    }
}
