using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaxHP", menuName = "ScriptableObjects/Upgrade/MaxHP")]
public class MaxHPSO : UpgradeSO
{
    public override void SetUpgrade()
    {
        base.SetUpgrade();
        _tower.SetMaxHP(_value);
    }
}
