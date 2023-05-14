using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPooling : Singleton<BrickPooling>
{
    [SerializeField] private Transform parent;
    [SerializeField] private int amount;
    [SerializeField] private GameObject brickPrefabs;
    private List<GameObject> poolObjects = new List<GameObject>();
    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(brickPrefabs, parent);
            obj.SetActive(false);
            poolObjects.Add(obj);
        }
    }

    public GameObject Spawn()
    {
        for (int i = 0; i < amount; i++)
        {
            if (!poolObjects[i].activeInHierarchy)
            {
                return poolObjects[i];
            }
        }
        return null;
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.parent = parent;

    }

}