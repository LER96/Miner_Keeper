using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EnumHandler;

public class Tower : MonoBehaviour
{
    [SerializeField] private TowerSO _towerData;
    [SerializeField] BulletType _currentType;

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

    [Header("Tower Variable")]
    [SerializeField] float _maxHP;
    [SerializeField] float _damage;
    [SerializeField] float _attackRate;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _maxCapacity;

    float _currentHP;
    float _currentTime;
    float _currentSpecialAmmoCapacity;
    Transform _target;
    TorretHandler _torretHandler;

    public float CurentHP=> _currentHP;
    public TorretHandler TorretHandler { get => _torretHandler; set => _torretHandler = value; }
    public Transform Target { get => _target; set => _target = value; }

    private void Update()
    {
        Timer();
    }

    private void FixedUpdate()
    {
        if (_target != null)
        {
            RotateTo();
        }
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

    public bool CanHeal()
    {
        if (_currentHP < _maxHP)
            return true;
        else
        {
            _currentHP = _maxHP;
            return false;
        }
    }



    public void ReloadSpecialAmmo(int amount)
    {
        _currentSpecialAmmoCapacity+=amount;
        _currentType = BulletType.Special;
        UpdateSpecailAmmo();
    }

    public void SetData(TowerSO data)
    {
        _towerData = data;
        _maxHP= PlayerManger.Instance.MaxHP;
        _currentHP= PlayerManger.Instance.CurrentHP;

        _damage = _towerData.Damage;
        _rotationSpeed = _towerData.Agility;
        _attackRate = 1/ _towerData.AttackRate;
        _towerBody.sprite = _towerData.TowerSprit;
        _gun.sprite = _towerData.GunSprite;
        _maxCapacity = _towerData.MaxCapacity;
        UpdateSpecailAmmo();
        UpdateHPBar();
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

    void UpdateHPBar()
    {
        float precent = _currentHP / _maxHP;
        _HpSlider.value = precent;
        _hpText.text= $"{_currentHP}/{_maxHP}";
    }

    void RotateTo()
    {
        Vector3 dir = (_target.position - _gunHolder.position);
        float _angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion desireRotation = Quaternion.Euler(0,0, _angle);
        _gunHolder.rotation = Quaternion.Slerp(_gunHolder.rotation, desireRotation, _rotationSpeed * Time.deltaTime);
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
            SetType();
            UpdateSpecailAmmo();

        }
    }

    void SetType()
    {
        if (_currentSpecialAmmoCapacity > 0)
        {
            _currentSpecialAmmoCapacity--;
        }
        else
        {
            _currentType = BulletType.Regular;
            _currentSpecialAmmoCapacity = 0;
        }
    }

    public void HpChange(float damage)
    {
        _currentHP+=damage;

        if (_currentHP < 0)
        {
            gameObject.SetActive(false);
        }
        else if (_currentHP > _maxHP)
            _currentHP = _maxHP;
        else
            UpdateHPBar();

        PlayerManger.Instance.CurrentHP = _currentHP;
    }

}
