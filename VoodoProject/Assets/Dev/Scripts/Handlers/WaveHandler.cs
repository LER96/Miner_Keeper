using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EnumHandler;
using static StructHandler;

public class WaveHandler : MonoBehaviour
{
    [SerializeField] int _amountOfEnemies;
    [SerializeField] Enemy _enemyPreFab;
    [SerializeField] Transform _enemyHolder;

    [SerializeField] List<WaveSO> _wavesData = new List<WaveSO>();
    private List<WaveVariables> _waveData = new List<WaveVariables>();
    private WaveType _waveType;
    private WaveSO _currentWave;
    //Counter Died
    private int _deathsToSpawnNext;
    private int _currentDied;

    private float _timerTillNextSpawn;
    //Wave spawn Rate
    private float _waveSpawnRate;
    private float _currentRate;
    //Wave Delay
    private float _currentDelay;
    private float _waveDelay;

    private bool _gameStart;
    private int _currentIndex;

    Queue<Enemy> _enemyQueue = new Queue<Enemy>();
    [SerializeField] List<Enemy> _enemyToActive = new List<Enemy>();
    List<Enemy> _activeEnemy = new List<Enemy>();

    private void Start()
    {
        InitPool();
        InitData(1);
    }
    private void Update()
    {
        if (_gameStart)
        {
            DecideSpawnBehavior();
        }
    }

    void InitPool()
    {
        for (int i = 0; i < _amountOfEnemies; i++)
        {
            Enemy enemy = Instantiate(_enemyPreFab, _enemyHolder);
            enemy.OnEnemyDied += DealEnemy;
            enemy.gameObject.SetActive(false);
            _enemyQueue.Enqueue(enemy);
        }
    }

    void InitData(int index)
    {
        if (index > _wavesData.Count)
            return;

        _currentRate = 0;
        _currentIndex = index;

        _currentWave = _wavesData[index-1];
        _waveData = _currentWave.WaveData;
        _waveType = _currentWave.WaveType;
        _timerTillNextSpawn = _currentWave.TimerNextWave;
        _deathsToSpawnNext = _currentWave.KillAmount;
        _waveDelay = _currentWave.WaveDelay;
        _currentDelay = 0;
        _currentDied = 0;

        SetSpawnList();
        _waveSpawnRate = _currentWave.SpawnRate;
        _gameStart = true;
    }

    void SetSpawnList()
    {
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
    }

    void DecideSpawnBehavior()
    {
        switch(_waveType)
        {
            case WaveType.KillingAmount:
                 if(_currentDied>= _deathsToSpawnNext)
                {
                    WaveDelayTimer();
                    return;
                }
                break;
            case WaveType.Timer:
                if(_enemyToActive.Count==0)
                {
                    WaveDelayTimer();
                    return;
                }
                break;
            case WaveType.Clear:
                if(_activeEnemy.Count == 0)
                {
                    WaveDelayTimer();
                }
                break;
        }
        DefaultWaveBehavior();
    }

    void WaveDelayTimer()
    {
        if(_currentDelay<_waveDelay)
        {
            _currentDelay += Time.deltaTime;
        }
        else
        {
            _currentDelay = 0;
            InitData(_currentIndex + 1);
        }
    }

    void DefaultWaveBehavior()
    {
        SpawnTimer();
        SetFirstTarget();
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
                    _currentDied++;
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
            UpgradeManager.Instance.TorretHandler.CurrentTower.Targets = _activeEnemy;
    }

}
