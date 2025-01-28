using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExoplosionUpgrade", menuName = "ScriptableObjects/Upgrade/Explostion")]
public class ExplosionSO : UpgradeSO
{
    public override void SetUpgrade()
    {
        base.SetUpgrade();
        _tower.ExplosionRadius += _value;
    }
}
