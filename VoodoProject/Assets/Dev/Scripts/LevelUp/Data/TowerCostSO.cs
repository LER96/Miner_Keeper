using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerCostSO", menuName = "ScriptableObjects/Tower/Cost")]
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
