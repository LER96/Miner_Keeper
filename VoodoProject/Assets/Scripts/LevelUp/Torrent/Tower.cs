using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    private TowerSO _towerData;

    [Header("Tower Object")]
    [SerializeField] Image _towerBody;
    [SerializeField] Image _gun;
    [SerializeField] Transform _gunHolder;
    [SerializeField] Transform _gunPoint;

    [Header("Tower Variable")]
    [SerializeField] float _damage;
    [SerializeField] float _attackRate;
    [SerializeField] float _rotationSpeed;

    TorretHandler _torretHandler;
    float _currentTime;

    public TorretHandler TorretHandler { set => _torretHandler = value; }

    private void Start()
    {
        _currentTime = _attackRate;
    }

    private void Update()
    {
        Timer();
    }

    void Timer()
    {
        if(_currentTime>0)
        {
            _currentTime -= Time.deltaTime;
        }
        else
        {
            Shoot();
            _currentTime = _attackRate;
        }
    }

    public void SetData(TowerSO data)
    {
        _damage = data.Damage;
        _attackRate = data.AttackRate;
        _towerBody.sprite = data.TowerSprit;
        _gun.sprite = data.GunSprite;
    }

    void Shoot()
    {
        if (_torretHandler != null)
        {
            Bullet bullet = _torretHandler.Bullets.Dequeue();
            bullet.gameObject.SetActive(true);
            bullet.Damage = _damage;
            bullet.transform.position = _gunPoint.position;
            bullet.transform.localEulerAngles = new Vector3(0,0,_gunHolder.eulerAngles.z-90);

            _torretHandler.Bullets.Enqueue(bullet);
        }
    }

}
