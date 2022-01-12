using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHp : MonoBehaviour
{
    public float Hp => hp;
    [SerializeField] private bool dieToDestroy;
    [SerializeField] protected float hp;
    [SerializeField] protected float maxHp = 100;
    protected virtual void OnEnable()
    {
        Initialize();
    }
    
    protected virtual void Initialize()
    {
        hp = maxHp;
    }
    public virtual void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            Die();
        }
    }
    protected virtual void Die()
    {
        if(dieToDestroy)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
