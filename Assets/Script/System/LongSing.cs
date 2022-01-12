using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongSing<T> : MonoBehaviour where T : Component
{
    public static T Instance
    {
        get;set;
    }
    protected virtual void Awake()
    {
        if(Instance == null)
            Instance = this as T;
        if(Instance != this)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
}
