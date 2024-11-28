using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class Gun : MonoBehaviour
{
    public enum TargetGun { closet, far, weakest, strongest}
    [SerializeField] private TargetGun targetGun;
    [SerializeField] private Transform _shootPoint;

    private float _rotationSpeed;
    private float distance;
    private float enemyHp;

    [SerializeField] private Enemy _target;
    private Enemy _tempTarget;
    [SerializeField] private List<Enemy> enemies;

    public void DecideTarget(List<Enemy> targets, float rotationSpeed)
    {
        enemies = targets;
        _rotationSpeed = rotationSpeed;
        if (enemies.Count > 0)
        {
            switch (targetGun)
            {
                case TargetGun.closet:
                    CheckDist();
                    break;
                case TargetGun.far:
                    CheckDist();
                    break;
                case TargetGun.weakest:
                    CheckEnemyHP();
                    break;
                case TargetGun.strongest:
                    CheckEnemyHP();
                    break;
            }
            if (_target != null)
                RotateTo();
        }
        else
        {
            _tempTarget = null;
            DisableTarget();
        }
    }

    #region Enemies Distance Check
    void CheckDist()
    {
        distance = enemies[0].Distance;
        _tempTarget = enemies[0];

        for (int i = 0; i < enemies.Count; i++)
        {
            if (targetGun == TargetGun.closet)
                Close(i);
            else if (targetGun == TargetGun.far)
                Far(i);
        }
        if (_tempTarget != null)
            SetTarget(_tempTarget);
    }

    void Close(int index)
    {
        float tempDit = enemies[index].Distance;
        if (tempDit < distance)
        {
            distance = tempDit;
            _tempTarget = enemies[index];
        }
    }

    void Far(int index)
    {
        float tempDit = enemies[index].Distance;
        if (tempDit > distance)
        {
            distance = tempDit;
            _tempTarget = enemies[index];
        }
    }
    #endregion

    #region Enemies Hp Check
    void CheckEnemyHP()
    {
        enemyHp = enemies[0].HP;
        _tempTarget = enemies[0];
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
            {
                if (targetGun == TargetGun.closet)
                    Weakest(i);
                else if (targetGun == TargetGun.far)
                    Strongest(i);
            }
        }
        if (_tempTarget != null)
            SetTarget(_tempTarget);

    }

    void Weakest(int index)
    {
        float tempHp = enemies[index].HP;
        if (tempHp < enemyHp)
        {
            enemyHp = tempHp;
            _tempTarget = enemies[index];
        }

    }

    void Strongest(int index)
    {
        float tempHp = enemies[index].HP;
        if (tempHp < enemyHp)
        {
            enemyHp = tempHp;
            _tempTarget = enemies[index];
        }
    }
    #endregion

    void SetTarget(int index)
    {
        if (enemies[index] != null)
        {
            _tempTarget = enemies[index];
            _target = enemies[index];
        }
    }

    void SetTarget(Enemy target)
    {
        _target = target;
    }

    void DisableTarget()
    {
        _target = null;
    }

    void RotateTo()
    {
        Vector3 dir = (_target.transform.position - transform.position);
        float _angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion desireRotation = Quaternion.Euler(0, 0, _angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, desireRotation, _rotationSpeed * Time.deltaTime);
    }

    public void ShootFrom(Transform bullet)
    {
        bullet.position = _shootPoint.position;
        bullet.localEulerAngles = new Vector3(0, 0, transform.eulerAngles.z - 90);
    }


}
