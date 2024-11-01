using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] TowerSO _towerData;
    [SerializeField] TowerCostSO _towerCostData;

    [Header("Tower Variable")]
    [SerializeField] float _damage;
    [SerializeField] float _attackRate;
    [SerializeField] float _rotationSpeed;


}
