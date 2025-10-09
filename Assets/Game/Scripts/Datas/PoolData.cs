using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolData
{
    public ItemTypes ItemType;
    public GameObject Prefab;
    public int InitialAmount;
    public int IncreaseAmount;
    public Queue<GameObject> PoolQueue;
}
