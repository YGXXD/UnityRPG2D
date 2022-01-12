using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpSystem : CharacterHp
{
    [SerializeField] private HpStateBar hpState;
    public override void TakeDamage(float damage)
    {
        PlayerAnimator.Instance.PlayerPlay("Hit");
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            hpState.UpdateState(hp, maxHp);
            Die();
            return;
        }
        hpState.UpdateState(hp, maxHp);
    }
    protected override void Die()
    {
        StartCoroutine(PlayerDie());
    }
    private IEnumerator PlayerDie()
    {
        PlayerAnimator.Instance.PlayerPlay("Death");
        yield return new WaitUntil(()=>PlayerAnimator.Instance.IsPlayed("Death"));
        base.Die();
    }
}
