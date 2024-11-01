using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCostSO : ScriptableObject
{
    [System.Serializable]
    public struct TowerItemsAmount
    {
        public ItemSO item;
        public int amount;
    }

    [SerializeField] List<TowerItemsAmount> _towerCostItems= new List<TowerItemsAmount>();

    public List<TowerItemsAmount> Cost=> _towerCostItems;
}
