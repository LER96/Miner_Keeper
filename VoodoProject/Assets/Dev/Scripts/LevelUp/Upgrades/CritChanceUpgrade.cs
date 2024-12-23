using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CritChance", menuName = "ScriptableObjects/Upgrade/CritChance")]
public class CritChanceUpgrade : UpgradeSO
{
    public override void SetUpgrade()
    {
        base.SetUpgrade();
        _tower.CritChance += _value;
    }
}
