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
        public TowerCostSO towerCost;
    }
    [SerializeField] int _levelIndex;
    [SerializeField] List<TowerBodyData> _towersBodies = new List<TowerBodyData>();
    private TowerCostSO _nextUpgrade;

    [Header("Tower Pool")]
    [SerializeField] int _bulletNumber;
    [SerializeField] Bullet _bulletPrefab;
    [SerializeField] Transform _bulletHolder;
    private Queue<Bullet> _bulletQueue = new Queue<Bullet>();

    public Queue<Bullet> Bullets => _bulletQueue;
    public TowerCostSO NextUpgrade => _nextUpgrade; 

    public void SetHandler()
    {
        InitPool();
        SetLevel(1);
    }

    void InitPool()
    {
        for (int i = 0; i < _bulletNumber; i++)
        {
            Bullet tower = Instantiate(_bulletPrefab, _bulletHolder);
            _bulletQueue.Enqueue(tower);
        }
    }

    public void SetLevel(int index)
    {
        _levelIndex = index;
        if(index> _towersBodies.Count)
            return;
        _towersBodies[index - 1].towerBody.gameObject.SetActive(true);
        CheckNextUpgrade(index);
        Tower tower = _towersBodies[index-1].towerBody;
        TowerSO data = _towersBodies[index-1].towerData;
        tower.TorretHandler = this;
        tower.SetData(data);
    }

    public void Upgrade()
    {
        _towersBodies[_levelIndex - 1].towerBody.gameObject.SetActive(false);
        _levelIndex++;
        SetLevel(_levelIndex);
    }

    void CheckNextUpgrade(int index)
    {
        if (index < _towersBodies.Count)
        {
            _nextUpgrade = _towersBodies[index].towerCost;
            if (_nextUpgrade != null)
                UpgradeManager.Instance.UpgradeHandler.CreateItems(_nextUpgrade);
        }
    }

}
