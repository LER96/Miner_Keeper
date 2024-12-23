using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackRateUpgrade", menuName = "ScriptableObjects/Upgrade/AttackRate")]
public class AttackRateUpgrade : UpgradeSO
{
    public override void SetUpgrade()
    {
        base.SetUpgrade();
        _tower.AttackRate += _value;
    }
}
