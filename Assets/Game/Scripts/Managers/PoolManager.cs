using Assets.Game.Scripts.Signals;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private List<PoolData> pools = new List<PoolData>();

    private Dictionary<ItemTypes, PoolData> poolDictionary;

    private void Awake()
    {
        poolDictionary = new Dictionary<ItemTypes, PoolData>();

        foreach (var pool in pools)
        {
            pool.PoolQueue = new Queue<GameObject>();

            for (int i = 0; i < pool.InitialAmount; i++)
            {
                CreateItem(pool);
            }

            poolDictionary.Add(pool.ItemType, pool);
        }
    }

    private void OnEnable()
    {
        PoolSignals.Instance.onGetItemFromPool += Get;
        PoolSignals.Instance.onItemReleased += Release;
    }
    private void OnDisable()
    {
        PoolSignals.Instance.onGetItemFromPool += Get;
        PoolSignals.Instance.onItemReleased -= Release;
    }

    private GameObject Get(ItemTypes type)
    {
        if (!poolDictionary.TryGetValue(type, out var pool))
        {
            Debug.LogWarning($"No pool found for {type}");
            return null;
        }

        if (pool.PoolQueue.Count == 0)
            ExpandPool(pool);

        GameObject obj = pool.PoolQueue.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    private void Release(ItemTypes type, GameObject obj)
    {
        if (!poolDictionary.TryGetValue(type, out var pool))
        {
            Destroy(obj);
            return;
        }

        obj.SetActive(false);
        pool.PoolQueue.Enqueue(obj);
    }

    private void ExpandPool(PoolData pool)
    {
        for (int i = 0; i < pool.IncreaseAmount; i++)
        {
            CreateItem(pool);
        }

        Debug.Log($"{pool.ItemType} expanded by {pool.IncreaseAmount}");
    }

    private void CreateItem(PoolData pool)
    {
        GameObject obj = Instantiate(pool.Prefab, transform);
        obj.SetActive(false);
        pool.PoolQueue.Enqueue(obj);
    }
}
