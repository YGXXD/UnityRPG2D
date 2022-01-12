using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 setoff;
    private void Start()
    {
        setoff = new Vector3(0, 3, -10);
        this.transform.position = player.position + setoff;
        if (player != null)
        {
            StartCoroutine(CameraFollowCoroutine());
        }
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    private IEnumerator CameraFollowCoroutine()
    {
        Vector3 target = new Vector3();
        while (player.gameObject.activeSelf)
        {
            target = player.position + setoff;
            yield return null;
            this.transform.position = Vector3.Lerp(this.transform.position, target, 0.06f);
        }
    }
    
}
