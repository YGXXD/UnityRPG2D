using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLifeTime : MonoBehaviour
{
    [SerializeField] bool isDestroy;
    [SerializeField] private float lifeTime;
    private WaitForSeconds Lifetime;
    private void Awake()
    {
        Lifetime = new WaitForSeconds(lifeTime);
    }
    private void OnEnable()
    {
        StartCoroutine(AutoEnd());
    }
    private IEnumerator AutoEnd()
    {
        yield return Lifetime;
        if (isDestroy)
            Destroy(this.gameObject);
        else
            this.gameObject.SetActive(false);
    }
}
