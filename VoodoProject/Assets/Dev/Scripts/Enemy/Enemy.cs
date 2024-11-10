using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Action<Enemy> OnEnemyDied;
    [SerializeField] protected EnemySO _enemyData;
    bool inRange;
    //[SerializeField] Slider _enemyHPBar;
    protected string _enemyName;
    protected float _enemyHp;
    protected float _enemySpeed;
    protected float _enemyDamage;
    protected float _enemyAttackRange;
    protected float _attackRate;

    protected float _currentAttackRate;
    protected float _maxHp;

    protected Transform _target;

    public EnemySO EnemyData => _enemyData;

    protected virtual void Start()
    {
        SetData(_enemyData);
    }

    public virtual void SetData(EnemySO enemySO)
    {
        _enemyData = enemySO;
        _enemyName = _enemyData.EnemyName;
        _enemyHp = _enemyData.HP;
        _maxHp = _enemyHp;
        _enemySpeed = _enemyData.MovementSpeed;
        _enemyDamage = _enemyData.Damage;
        _enemyAttackRange = _enemyData.AttackRange*100;
        _attackRate = 1/_enemyData.AttackRate;
    }

    protected virtual void InitData()
    {
        _enemyHp = _maxHp;
        _currentAttackRate = 0;
        transform.localPosition = Vector3.zero;

    }

    private void FixedUpdate()
    {
        Decide();
    }

    void Decide()
    {
        _target = UpgradeManager.Instance.TorretHandler.CurrentTower.transform;
        float dist = Vector2.Distance(transform.position, _target.position);
        if (dist < _enemyAttackRange)
        {
            inRange = true;
            AttackCD();
        }
        else
        {
            inRange = false;
            Move();
        }
    }

    protected void Move()
    {
        transform.localPosition -= new Vector3(_enemySpeed, 0, 0);

    }

    protected void AttackCD()
    {
        if (_currentAttackRate < _attackRate)
        {
            _currentAttackRate += Time.deltaTime;
        }
        else
        {
            _currentAttackRate = 0;
            Attack();
        }
    }

    protected virtual void Attack()
    {

    }

    public virtual void TakeDamage(float dmg)
    {
        _enemyHp -= dmg;
        //Hit VFX
        if(_enemyHp<0)
        {
            _enemyHp = 0;
            OnEnemyDied.Invoke(this);
            gameObject.SetActive(false);
        }
        
    }

    protected virtual void OnDisable()
    {
        InitData();
    }
}
