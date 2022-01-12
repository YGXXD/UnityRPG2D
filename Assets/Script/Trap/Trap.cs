using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private GameObject hitPlayerVFX;
    [SerializeField] private GameObject hitThisVFX;
    // [SerializeField] private float speed;
    private Rigidbody2D thisRigid;

    private void Awake()
    {
        thisRigid = GetComponent<Rigidbody2D>();
        thisRigid.bodyType = RigidbodyType2D.Static;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            // StartCoroutine(FallCoroutine());
            thisRigid.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerHpSystem player))
        {
            if (player.Hp > 0)
            {
                player.TakeDamage(20f);
                PoolManager.Realease(hitPlayerVFX, other.GetContact(0).point, Quaternion.identity);
            }
        }
        PoolManager.Realease(hitThisVFX, other.GetContact(0).point, Quaternion.identity);
        this.gameObject.SetActive(false);
    }

    // private IEnumerator FallCoroutine()
    // {
    //     while (this.gameObject.activeSelf)
    //     {
    //         this.transform.Translate(speed * Vector2.down * Time.deltaTime);
    //         yield return null;
    //     }
    // }
}
