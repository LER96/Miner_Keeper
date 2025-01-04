using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Omni", menuName = "ScriptableObjects/Upgrade/LifeSteal")]
public class LifeStealUpgrade : UpgradeSO
{
    public override void SetUpgrade()
    {
        base.SetUpgrade();
        _tower.LifeSteal += _value;
    }
}
