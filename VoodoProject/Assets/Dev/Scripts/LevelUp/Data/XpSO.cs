using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "XP", menuName = "ScriptableObjects/XP")]
public class XpSO : ScriptableObject
{
    [SerializeField] float _xpPoints;
    [SerializeField] List<UpgradeSO> _upgrades = new List<UpgradeSO>();

    public float XPCapacity => _xpPoints;
    public List<UpgradeSO> Upgrades => _upgrades;
}
