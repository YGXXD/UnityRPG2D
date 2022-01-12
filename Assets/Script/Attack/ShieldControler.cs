using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldControler : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private void OnEnable()
    {
        playerTransform = PlayerSkillSystem.Instance.GetSkill(7).ReleaseTransform;
    }

    private void Update()
    {
        this.transform.position = playerTransform.position;
    }
}
