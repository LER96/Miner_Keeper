using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static StructHandler;

public class Enemy : MonoBehaviour
{
    public Action<Enemy> OnEnemyDied;
    [SerializeField] Slider _enemyHPBar;
    //[SerializeField] List<EnemyVariable> _enemyVariable = new List<EnemyVariable>();
    [SerializeField] protected EnemySO _enemyData;

    protected bool inRange;
    protected string _enemyName;
    protected float _enemyHp;
    protected float _enemySpeed;
    protected float _enemyDamage;
    protected float _enemyAttackRange;
    protected float _attackRate;

    protected float _distance;
    protected float _currentAttackRate;
    protected float _maxHp;

    protected Tower _target;

    public EnemySO EnemyData => _enemyData;
    public float HP => _enemyHp;
    public float Distance => _distance;

    //protected virtual void Start()
    //{
    //    SetData(_enemyData);
    //}

    private void OnEnable()
    {
        _enemyHPBar.gameObject.SetActive(true);
        SetData(_enemyData);
    }

    public virtual void SetBody(EnemySO enemy)
    {
        //for (int i = 0; i < _enemyVariable.Count; i++)
        //{
        //    EnemySO _enemy = _enemyVariable[i].EnemyData;
        //    if (enemy.EnemyName == _enemy.EnemyName)
        //    {
        //        _enemyVariable[i].EnemyBody.gameObject.SetActive(true);
        //    }
        //    else
        //        _enemyVariable[i].EnemyBody.gameObject.SetActive(false);
        //}
        SetData(enemy);
    }

    protected virtual void SetData(EnemySO enemySO)
    {
        _enemyData = enemySO;
        _enemyName = _enemyData.EnemyName;
        _enemyHp = _enemyData.HP;
        _maxHp = _enemyHp;
        _enemySpeed = _enemyData.MovementSpeed;
        _enemyDamage = _enemyData.Damage;
        _enemyAttackRange = _enemyData.AttackRange*100;
        _attackRate = _enemyData.AttackRate;

        HPSlider();
    }

    protected virtual void InitData()
    {
        _enemyHp = _maxHp;
        _currentAttackRate = _attackRate;
        transform.localPosition = new Vector3(0,25,0);
        HPSlider();

    }

    protected virtual void FixedUpdate()
    {
        _target = UpgradeManager.Instance.TorretHandler.CurrentTower;
        CheckDistance();
        Decide();
    }

    protected void CheckDistance()
    {
        Vector3 targetPos = new Vector3(_target.transform.position.x, transform.position.y, 0);
        _distance = Vector3.Distance(transform.position, targetPos);
    }

    protected virtual void  Decide()
    {
        float dist = Vector2.Distance(transform.position, _target.transform.position);
        if (dist <= _enemyAttackRange)
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

    protected virtual void Move()
    {
        transform.localPosition -= new Vector3(_enemySpeed*10*Time.deltaTime, 0, 0);

    }

    protected virtual void AttackCD()
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
        _target.HpChange(-_enemyDamage); 
    }

    public virtual void TakeDamage(float dmg)
    {
        _enemyHp -= dmg;
        HPSlider();
        //Hit VFX
        if (_enemyHp<0)
        {
            UpgradeManager.Instance.UpgradeHandler.DropItem(_enemyData.DropItem, this.transform);
            HandleDeath();
        }
        
    }

    protected virtual void HandleDeath()
    {
        _enemyHp = 0;
        InitData();
        OnEnemyDied.Invoke(this);
        gameObject.SetActive(false);
    }

    protected virtual void HPSlider()
    {
        _enemyHPBar.value = _enemyHp / _maxHp;
    }

    protected virtual void OnDisable()
    {
        InitData();
    }
}
