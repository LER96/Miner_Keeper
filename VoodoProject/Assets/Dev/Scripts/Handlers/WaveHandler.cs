using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static StructHandler;

public class WaveHandler : MonoBehaviour
{
    [SerializeField] int _amountOfEnemies;
    [SerializeField] Enemy _enemyPreFab;
    [SerializeField] Transform _enemyHolder;

    [SerializeField] List<WaveSO> _wavesData = new List<WaveSO>();
    private List<WaveVariables> _waveData = new List<WaveVariables>();
    private WaveSO _currentWave;
    private float _waveSpawnRate=3;
    private float _currentRate;
    private bool _gameStart;
    private int _currentIndex = 1;

    Queue<Enemy> _enemyQueue = new Queue<Enemy>();
    [SerializeField] List<Enemy> _enemyToActive = new List<Enemy>();
    List<Enemy> _activeEnemy = new List<Enemy>();

    private void Start()
    {
        InitPool();
        SetData(1);
    }

    void InitPool()
    {
        for (int i = 0; i < _amountOfEnemies; i++)
        {
            Enemy enemy = Instantiate(_enemyPreFab, _enemyHolder);
            enemy.OnEnemyDied += DealEnemy;
            _enemyQueue.Enqueue(enemy);
        }
    }

    void SetData(int index)
    {
        if (index > _wavesData.Count)
            return;

        _currentIndex = index;
        _currentWave = _wavesData[index-1];
        _waveData = _currentWave.WaveData;

        for (int i = 0; i < _waveData.Count; i++)
        {
            int numberToSpawn = _waveData[i].enemyNumber;
            for (int j = 0; j < numberToSpawn; j++)
            {
                Enemy enemy = _enemyQueue.Dequeue();
                enemy.SetBody(_waveData[i].enemydata);
                _enemyToActive.Add(enemy);
                _enemyQueue.Enqueue(enemy);
            }
        }

        _waveSpawnRate = _currentWave.SpawnRate;
        _gameStart = true;
    }

    private void Update()
    {
        if (_gameStart)
        {
            SpawnTimer();
            SetFirstTarget();
        }
    }

    void SpawnTimer()
    {
        if (_currentRate < _waveSpawnRate)
        {
            _currentRate += Time.deltaTime;
        }
        else
        {
            _currentRate = 0;
            Spawn();
        }
    }

    void DealEnemy(Enemy enemy)
    {
        if (_activeEnemy.Contains(enemy))
        {
            for (int i = 0; i < _activeEnemy.Count; i++)
            {
                if (_activeEnemy[i] == enemy)
                {
                    _activeEnemy.Remove(_activeEnemy[i]);
                    if (_activeEnemy.Count == 0)
                        SetData(_currentIndex + 1);
                    return;
                }  
            }
        }
    }

    void Spawn()
    {
        if (_enemyToActive.Count == 0)
            return;

        int rndPos = 0;
        if (_enemyToActive.Count > 1)
        {
            rndPos = Random.Range(0, _enemyToActive.Count);
        }
        else
        {
            rndPos = 0;
        }

        Enemy enemy = _enemyToActive[rndPos];
        _enemyToActive.Remove(_enemyToActive[rndPos]);
        enemy.gameObject.SetActive(true);
        _activeEnemy.Add(enemy);
    }

    void SetFirstTarget()
    {
        if (_activeEnemy.Count > 0)
            UpgradeManager.Instance.TorretHandler.CurrentTower.Target = _activeEnemy[0].transform;
    }

}
