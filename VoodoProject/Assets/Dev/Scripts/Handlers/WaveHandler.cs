using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StructHandler;

public class WaveHandler : MonoBehaviour
{
    [SerializeField] int _amountOfEnemies;
    [SerializeField] Enemy _enemyPreFab;
    [SerializeField] Transform _enemyHolder;

    [SerializeField] List<WaveSO> _wavesData = new List<WaveSO>();
    private WaveSO _currentWave;
    private float _waveSpawnRate=3;
    private float _currentRate;

    Queue<Enemy> _enemyQueue = new Queue<Enemy>();
    List<Enemy> _activeEnemy = new List<Enemy>();

    private void Start()
    {
        InitPool();
        _currentRate = _waveSpawnRate;
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

        _currentWave = _wavesData[index-1];
        _waveSpawnRate = _currentWave.SpawnRate;


    }

    private void Update()
    {
        SpawnTimer();
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
                    SetFirstTarget();
                    return;
                }   
            }
        }

    }

    void Spawn()
    {
        Enemy enemy = _enemyQueue.Dequeue();
        enemy.gameObject.SetActive(true);
        _activeEnemy.Add(enemy);
        SetFirstTarget();
        _enemyQueue.Enqueue(enemy);
    }

    void SetFirstTarget()
    {
        UpgradeManager.Instance.TorretHandler.CurrentTower.Target = _activeEnemy[0].transform;
    }

}
