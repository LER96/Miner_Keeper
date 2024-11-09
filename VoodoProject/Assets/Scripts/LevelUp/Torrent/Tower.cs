using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EnumHandler;

public class Tower : MonoBehaviour
{
    [SerializeField] private TowerSO _towerData;

    [Header("Tower Object")]
    [SerializeField] Image _towerBody;
    [SerializeField] Image _gun;
    [SerializeField] Transform _gunHolder;
    [SerializeField] Transform _gunPoint;

    [Header("Slider")]
    [SerializeField] Slider _specialAmmoSlider;
    [SerializeField] Slider _HpSlider;
    [SerializeField] TMP_Text _hpText;
    [SerializeField] TMP_Text _ammoText;
    BulletType _currentType;

    [Header("Tower Variable")]
    [SerializeField] float _damage;
    [SerializeField] float _attackRate;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _maxCapacity;

    float _currentTime;
    float _currentSpecialAmmoCapacity;
    TorretHandler _torretHandler;

    public TorretHandler TorretHandler { get => _torretHandler; set => _torretHandler = value; }

    private void Update()
    {
        Timer();
    }


    public bool CanRelaod()
    {
        if (_currentSpecialAmmoCapacity < _maxCapacity)
            return true;
        else
        {
            _currentSpecialAmmoCapacity = _maxCapacity;
            return false;
        }
    }
    public void ReloadSpecialAmmo(int amount)
    {
        _currentSpecialAmmoCapacity+=amount;
        UpdateSpecailAmmo();
    }

    public void SetData(TowerSO data)
    {
        _towerData = data;
        _damage = _towerData.Damage;
        _attackRate = 1/ _towerData.AttackRate;
        _towerBody.sprite = _towerData.TowerSprit;
        _gun.sprite = _towerData.GunSprite;
        _maxCapacity = _towerData.MaxCapacity;
        UpdateSpecailAmmo();
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

    void UpdateSpecailAmmo()
    {
        float precnet = _currentSpecialAmmoCapacity / _maxCapacity;
        _specialAmmoSlider.value = precnet;
        _ammoText.text = $"{_currentSpecialAmmoCapacity}/{_maxCapacity}";
    }

    void Shoot()
    {
        if (_torretHandler != null)
        {
            Bullet bullet = _torretHandler.Bullets.Dequeue();
            bullet.gameObject.SetActive(true);
            bullet.SetBulletType(_currentType);
            bullet.transform.position = _gunPoint.position;
            bullet.transform.localEulerAngles = new Vector3(0,0,_gunHolder.eulerAngles.z-90);

            _torretHandler.Bullets.Enqueue(bullet);
            if (_currentSpecialAmmoCapacity > 0)
            {
                _currentType = BulletType.Special;
                _currentSpecialAmmoCapacity--;
            }
            else
            {
                _currentType = BulletType.Regular;
                _currentSpecialAmmoCapacity = 0;
            }
            UpdateSpecailAmmo();

        }
    }

}
