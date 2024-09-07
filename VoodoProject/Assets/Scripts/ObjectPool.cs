using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField] GameObject objPrefab;
    [SerializeField] int amountToPool;
    private List<GameObject> pooledObjs = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(objPrefab, this.transform);
            obj.SetActive(false);
            pooledObjs.Add(obj);
        }
    }

    public GameObject GetPoolObj()
    {
        for (int i = 0; i < pooledObjs.Count; i++)
        {
            if(pooledObjs[i].activeInHierarchy==false)
            {
                return pooledObjs[i];
            }
        }
        return null;
    }
}
