using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private List<Pool> pools;
    public static Dictionary<GameObject, Pool> dictionary = new Dictionary<GameObject, Pool>();

    private void Awake()
    {
        Initialize(pools);
    }
    private void Initialize(List<Pool> pools)
    {
        foreach(Pool item in pools)
        {
#if UNITY_EDITOR
            if (dictionary.ContainsKey(item.Prefab))
            {
                Debug.LogError("The Prefab Contains:" + item.Prefab.name);
                continue;
            }
#endif
            dictionary.Add(item.Prefab, item);
            GameObject prefabPool = new GameObject("Pool " + item.Prefab.name + ":");
            prefabPool.transform.parent = this.transform;
            item.Initialize(prefabPool.transform);
        }
    }
    public static GameObject Realease(GameObject prefab)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Not Find The Prefab In Pool");
        }
#endif
        return dictionary[prefab].ActivePrefab();
    }
    public static GameObject Realease(GameObject prefab, Vector3 position)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Not Find The Prefab In Pool");
        }
#endif
        return dictionary[prefab].ActivePrefab(position);
    }
    public static GameObject Realease(GameObject prefab, Vector3 position, Quaternion rotation)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Not Find The Prefab In Pool");
        }
#endif
        return dictionary[prefab].ActivePrefab(position,rotation);
    }
    public static GameObject Realease(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Not Find The Prefab In Pool");
        }
#endif
        return dictionary[prefab].ActivePrefab(position, rotation, scale);
    }
}
