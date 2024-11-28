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
                    CheckDist();
                    break;
                case TargetGun.strongest:
                    CheckDist();
                    break;
            }
            if (_target != null)
                RotateTo();
        }
    }

    #region Enemies Distance Check
    void CheckDist()
    {
        if (enemies.Count > 1 && _target==null)
        {
            float distance = Vector3.Distance(transform.position, enemies[0].transform.position);
            for (int i = 1; i < enemies.Count; i++)
            {
                if (targetGun == TargetGun.closet)
                    Close(distance, i);
                else if (targetGun == TargetGun.far)
                    Far(distance, i);
            }

            if (_tempTarget != null )
                SetTarget(_tempTarget);
        }
        else if (enemies.Count == 1 && _target == null)
            SetTarget(0);
        else if (_target != null && _target.HP <= 0)
            DisableTarget();
    }

    void Close(float distance, int index)
    {
        float tempDit = Vector3.Distance(transform.position, enemies[index].transform.position);
        if (tempDit < distance)
            _tempTarget = enemies[index];
    }

    void Far(float distance, int index)
    {
        float tempDit = Vector3.Distance(transform.position, enemies[index].transform.position);
        if (tempDit > distance)
            _tempTarget = enemies[index];
    }
    #endregion

    #region Enemies Hp Check
    void CheckEnemyHP()
    {
        if (enemies.Count > 1 && _target == null)
        {
            float enemyHp = enemies[0].HP;
            for (int i = 1; i < enemies.Count; i++)
            {
                if (targetGun == TargetGun.closet)
                    Weakest(enemyHp, i);
                else if (targetGun == TargetGun.far)
                    Strongest(enemyHp, i);
            }

            if (_tempTarget != null)
                SetTarget(_tempTarget);
        }
        else if (enemies.Count == 1 && _target == null)
            SetTarget(0);
        else if (_target != null && _target.HP<=0)
            DisableTarget();

    }

    void Weakest(float hp, int index)
    {
        float tempHp = enemies[index].HP;
        if (tempHp < hp)
            _tempTarget = enemies[index];
    }

    void Strongest(float hp, int index)
    {
        float tempHp = enemies[index].HP;
        if (tempHp > hp)
            _tempTarget = enemies[index];
    }
    #endregion

    void SetTarget(int index)
    {
        _target= enemies[index];
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
