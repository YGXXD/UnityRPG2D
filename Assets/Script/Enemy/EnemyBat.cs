using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : EnemyControler
{
    private Rigidbody2D batbody;
    private Vector2 maxVector;
    private Vector2 minVector;
    private Vector2 randomTarget;
    private IEnumerator moveCtrl;
    private void Awake()
    {
        Vector2 vector = new Vector2(2, 2);
        batbody = this.GetComponent<Rigidbody2D>();
        maxVector = (Vector2)this.transform.position + vector;
        minVector = (Vector2)this.transform.position - vector;
        moveCtrl = RandomMoveCoroutine(minVector, maxVector);
    }
    private void Start()
    {
        StartCoroutine(moveCtrl);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHpSystem>(out PlayerHpSystem player))
        {
            StopCoroutine(moveCtrl);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)//触发每帧都会做
    {
        if (collision.TryGetComponent<PlayerHpSystem>(out PlayerHpSystem player))
        {
            batbody.velocity = (player.transform.position - this.transform.position).normalized * speed;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.gameObject.activeSelf)
        {
            if (collision.TryGetComponent<PlayerHpSystem>(out PlayerHpSystem player))
            {
                StartCoroutine(moveCtrl);
            }
        }
    }
    private IEnumerator RandomMoveCoroutine(Vector2 min, Vector2 max)
    {
        while(this.gameObject.activeSelf)
        {
            randomTarget.x = Random.Range(min.x, max.x);
            randomTarget.y = Random.Range(min.y, max.y);
            while (this.gameObject.activeSelf && Vector2.Distance((Vector2)this.transform.position, randomTarget) > 0.1f)
            {
                batbody.velocity = (randomTarget - (Vector2)this.transform.position).normalized * speed;
                yield return null;
            }
        }
    }
}
