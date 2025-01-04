using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretHandler : MonoBehaviour
{

    [SerializeField] int _levelIndex;
    [SerializeField] Tower _tower;
    [SerializeField] List<TowerSO> _towersBodies = new List<TowerSO>();

    [Header("Tower Pool")]
    [SerializeField] int _bulletNumber;
    [SerializeField] Bullet _bulletPrefab;
    [SerializeField] Transform _bulletHolder;

    private Tower _currentTower;
    private TowerSO _currentData;
    private TowerCostSO _nextUpgrade;
    private Queue<Bullet> _bulletQueue = new Queue<Bullet>();

    public Queue<Bullet> Bullets => _bulletQueue;
    public TowerCostSO NextUpgrade => _nextUpgrade;
    public Tower CurrentTower => _currentTower;

    public void SetHandler()
    {
        InitPool();
        _currentTower = _tower;
        SetLevelData(1);
    }

    public void Upgrade()
    {
        PlayerManger.Instance.CurrentHP = _currentTower.CurentHP;
        _levelIndex++;
        SetLevelData(_levelIndex);
    }

    public void SetLevelData(int index)
    {

        if (index > _towersBodies.Count)
        {
            //ClearUpgradeSlots();
            return;
        }
        _levelIndex = index;
        //CheckNextUpgrade(index);
        _currentData = _towersBodies[index - 1];
        _currentTower.TorretHandler = this;
        _currentTower.SetData(_currentData);
    }

    void InitPool()
    {
        for (int i = 0; i < _bulletNumber; i++)
        {
            Bullet tower = Instantiate(_bulletPrefab, _bulletHolder);
            _bulletQueue.Enqueue(tower);
        }
    }

    #region Upgrade Slot- Not in Use
    //void CheckNextUpgrade(int index)
    //{
    //    if (index < _towersBodies.Count)
    //    {
    //        _nextUpgrade = _towersBodies[index].towerData.TowerCost;
    //        if (_nextUpgrade != null)
    //            UpgradeManager.Instance.TowerUpgradeHandler.CreateItems(_nextUpgrade);
    //        else
    //            ClearUpgradeSlots();
    //    }
    //    else
    //        ClearUpgradeSlots();
    //}

    //void ClearUpgradeSlots()
    //{
    //    UpgradeManager.Instance.TowerUpgradeHandler.ClearAll();
    //}
    #endregion
}
