using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CritMultiplier", menuName = "ScriptableObjects/Upgrade/CritMultiplier")]
public class CritMultiplierUpgrade : UpgradeSO
{
    public override void SetUpgrade()
    {
        base.SetUpgrade();
        _tower.CritChance += _value;
    }
}
