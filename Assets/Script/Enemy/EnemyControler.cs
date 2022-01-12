using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float collisionDamage;
    [SerializeField] private GameObject hitPlayerVFX;
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHpSystem>(out PlayerHpSystem player))
        {
            if (player.Hp > 0)
            {
                if (collision.GetContact(0).normal.x > 0)
                    player.transform.rotation = Quaternion.Euler(0, 180, 0);
                else
                    player.transform.rotation = Quaternion.Euler(0, 0, 0);
                player.TakeDamage(collisionDamage);
                PoolManager.Realease(hitPlayerVFX, collision.GetContact(0).point, Quaternion.identity);
            }
        }
    }
}
