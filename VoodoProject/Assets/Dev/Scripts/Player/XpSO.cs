using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "XP", menuName = "ScriptableObjects/XP")]
public class XpSO : ScriptableObject
{
    [SerializeField] float _xpPoints;

    public float XPCapacity => _xpPoints;
}
