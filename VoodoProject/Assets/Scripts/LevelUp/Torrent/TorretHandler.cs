using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretHandler : MonoBehaviour
{
    [System.Serializable]
    public struct TowerBodyData
    {
        public Tower towerBody;
        public TowerSO towerData;
    }
    [SerializeField] int _levelIndex;
    [SerializeField] List<TowerBodyData> _towersBodies = new List<TowerBodyData>();

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
        SetLevelData(1);
    }

    public void Upgrade()
    {
        _towersBodies[_levelIndex - 1].towerBody.gameObject.SetActive(false);
        _levelIndex++;
        SetLevelData(_levelIndex);
    }

    public void SetLevelData(int index)
    {
        _levelIndex = index;
        if (index > _towersBodies.Count)
        {
            ClearUpgradeSlots();
            return;
        }
        _towersBodies[index - 1].towerBody.gameObject.SetActive(true);
        CheckNextUpgrade(index);
        _currentTower = _towersBodies[index - 1].towerBody;
        _currentData = _towersBodies[index - 1].towerData;
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

    void CheckNextUpgrade(int index)
    {
        if (index < _towersBodies.Count)
        {
            _nextUpgrade = _towersBodies[index].towerData.TowerCost;
            if (_nextUpgrade != null)
                UpgradeManager.Instance.UpgradeHandler.CreateItems(_nextUpgrade);
            else
                ClearUpgradeSlots();
        }
        else
            ClearUpgradeSlots();
    }

    void ClearUpgradeSlots()
    {
        UpgradeManager.Instance.UpgradeHandler.ClearAll();
    }

}
