using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]public class Pool
{
    public GameObject Prefab => prefab;
    private Queue<GameObject> pool;
    [SerializeField] private GameObject prefab;
    private Transform parent;

    public Pool(GameObject gameObject)
    {
        this.prefab = gameObject;
    }
    public void Initialize(Transform parent)
    {
        this.parent = parent;
        pool = new Queue<GameObject>();
        pool.Enqueue(Copy());
    }
    private GameObject Copy()
    {
        GameObject item = GameObject.Instantiate(prefab);
        item.SetActive(false);
        item.transform.parent = parent;
        return item;
    }
    private GameObject SelectOnePrefab()
    {
        GameObject select = null;
        if (pool.Count > 0 && !pool.Peek().activeSelf)
        {
            select = pool.Dequeue();
        }
        else
        {
            select = Copy();
        }
        pool.Enqueue(select);
        return select;
    }
    public GameObject ActivePrefab()
    {
        GameObject activeprefab = SelectOnePrefab();
        activeprefab.SetActive(true);
        return activeprefab;
    }
    public GameObject ActivePrefab(Vector3 position)
    {
        GameObject activeprefab = SelectOnePrefab();
        activeprefab.SetActive(true);
        activeprefab.transform.position = position;
        return activeprefab;
    }
    public GameObject ActivePrefab(Vector3 position,Quaternion rotation)
    {
        GameObject activeprefab = SelectOnePrefab();
        activeprefab.SetActive(true);
        activeprefab.transform.position = position;
        activeprefab.transform.rotation = rotation;
        return activeprefab;
    }
    public GameObject ActivePrefab(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        GameObject activeprefab = SelectOnePrefab();
        activeprefab.SetActive(true);
        activeprefab.transform.position = position;
        activeprefab.transform.rotation = rotation;
        activeprefab.transform.localScale = scale;
        return activeprefab;
    }
}
