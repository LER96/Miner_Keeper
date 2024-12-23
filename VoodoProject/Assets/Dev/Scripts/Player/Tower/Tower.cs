using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EnumHandler;

public class Tower : MonoBehaviour
{

    [Header("Tower Object")]
    [SerializeField] Image _towerBody;
    [SerializeField] Image _gun;
    [SerializeField] List<Gun> _gunHolders = new List<Gun>();
    int index=0;

    [Header("Slider")]
    [SerializeField] Slider _specialAmmoSlider;
    [SerializeField] Slider _HpSlider;
    [SerializeField] TMP_Text _hpText;
    [SerializeField] TMP_Text _ammoText;

    private TowerSO _towerData;
    BulletType _currentType;

    //[Header("Tower Variable")]
    private float _maxHP;
    private float _damage;
    private float _omniPower;
    private float _specialDamage;
    private float _explosion;
    private float _critChance;
    private float _critMultiplier;
    private float _attackRate;
    private float _rotationSpeed;
    private float _maxCapacity;

    float _currentHP;
    float _currentTime;
    float _currentSpecialAmmoCapacity;
    List<Enemy> _targets= new List<Enemy>();
    TorretHandler _torretHandler;

    #region Getter/Setter
    public float CurentHP=> _currentHP;
    public float Damage { get => _damage; set => _damage = value; }
    public float ExplosionRadius { get => _explosion; set => _explosion = value; }
    public float LifeSteal { get => _omniPower; set => _omniPower = value; }
    public float CritChance { get => _critChance; set => _critChance = value; }
    public float CritMultiplier { get => _critMultiplier; set => _critMultiplier = value; }
    public float AttackRate { get => _attackRate; set => _attackRate = value; }
    public TorretHandler TorretHandler { get => _torretHandler; set => _torretHandler = value; }
    public List<Enemy> Targets { get => _targets; set => _targets = value; }
    #endregion

    private void Update()
    {
        Timer();
    }

    private void FixedUpdate()
    {
        RotateTo();
    }

    public void SetMaxHP(float hp)
    {
        _maxHP += hp;
        HpChange(hp);
    }

    public void SetData(TowerSO data)
    {
        _towerData = data;
        _maxHP= PlayerManger.Instance.MaxHP;
        _currentHP= PlayerManger.Instance.CurrentHP;
        _omniPower = _towerData.Omni;
        _explosion = _towerData.ExplosionRadius;
        _critChance = _towerData.CritChance;
        _critMultiplier = _towerData.CritMultiplier;
        _damage = _towerData.Damage;
        _specialDamage = _towerData.SpecialDamage;
        _rotationSpeed = _towerData.Agility;
        _attackRate = _towerData.AttackRate;
        _towerBody.sprite = _towerData.TowerSprit;
        _gun.sprite = _towerData.GunSprite;
        _maxCapacity = _towerData.MaxCapacity;

        UpdateSpecailAmmo();
        UpdateHPBar();
    }

    #region Shooting: Attack Rate, Rotate, Pool Shooting
    void Timer()
    {
        if(_currentTime>0)
        {
            _currentTime -= Time.deltaTime;
        }
        else
        {
            Shoot();
            _currentTime = 1/_attackRate;
        }
    }

    void RotateTo()
    {
        for (int i = 0; i < _gunHolders.Count; i++)
        {
            _gunHolders[i].DecideTarget(_targets, _rotationSpeed);
        }
    }

    void Shoot()
    {
        if (_torretHandler != null)
        {
            SetType();
            UpdateSpecailAmmo();

            for (int i = 0; i < _gunHolders.Count; i++)
            {
                Bullet bullet = _torretHandler.Bullets.Dequeue();
                bullet.gameObject.SetActive(true);
                SetBulletDamage(bullet);
                bullet.Omni = _omniPower;
                bullet.Explosion = _explosion;
                bullet.SetBulletType(_currentType);

                _gunHolders[i].ShootFrom(bullet.transform);
                _torretHandler.Bullets.Enqueue(bullet);
            }

        }
    }

    void SetBulletDamage(Bullet bullet)
    {
        if (IsCrit())
        {
            bullet.Damage = _damage * _critMultiplier;
        }
        else
            bullet.Damage = _damage;
    }
    #endregion

    #region Updating HP, Special Bullet Bars
    public void ReloadSpecialAmmo(int amount)
    {
        _currentSpecialAmmoCapacity+=amount;
        if (_currentSpecialAmmoCapacity >= _maxCapacity)
            _currentSpecialAmmoCapacity = _maxCapacity;
        _currentType = BulletType.Special;
        UpdateSpecailAmmo();
    }

    void UpdateSpecailAmmo()
    {
        float precnet = _currentSpecialAmmoCapacity / _maxCapacity;
        _specialAmmoSlider.value = precnet;
        _ammoText.text = $"{_currentSpecialAmmoCapacity}/{_maxCapacity}";
    }

    public void HpChange(float damage)
    {
        _currentHP += damage;

        if (_currentHP < 0)
        {
            gameObject.SetActive(false);
        }
        else if (_currentHP >= _maxHP)
            _currentHP = _maxHP;

        UpdateHPBar();
        PlayerManger.Instance.CurrentHP = _currentHP;
    }

    void UpdateHPBar()
    {
        float precent = _currentHP / _maxHP;
        _HpSlider.value = precent;
        _hpText.text= $"{_currentHP}/{_maxHP}";
    }
    #endregion

    void SetType()
    {
        if (_currentSpecialAmmoCapacity > 0)
        {
            _damage = _specialDamage;
            _currentSpecialAmmoCapacity--;
        }
        else
        {
            _currentType = BulletType.Regular;
            _damage = _towerData.Damage;
            _currentSpecialAmmoCapacity = 0;
        }
    }

    bool IsCrit()
    {
        float precent = Random.Range(0, 100);
        if(precent<_critChance)
        {
            return true;
        }
        return false;
    }
}
