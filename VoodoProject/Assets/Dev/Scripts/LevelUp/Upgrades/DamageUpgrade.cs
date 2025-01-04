using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageUpgrade", menuName = "ScriptableObjects/Upgrade/Damage")]
public class DamageUpgrade : UpgradeSO
{
    public override void SetUpgrade()
    {
        base.SetUpgrade();
        _tower.Damage += _value;
    }
}
