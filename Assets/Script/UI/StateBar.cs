using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateBar : MonoBehaviour
{
    [SerializeField] protected Image imageFront;
    protected float state;
    protected virtual void Awake()
    {
        if (this.TryGetComponent<Canvas>(out Canvas canvas))
        {
            canvas.worldCamera = Camera.main;
        }
    }
    public virtual void UpdateState(float nowState, float maxState)
    {
        state = nowState / maxState;
        imageFront.fillAmount = state;
    }
}
