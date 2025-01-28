using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static EnumHandler;
using static StructHandler;

public class WaveHandler : MonoBehaviour
{
    [SerializeField] List<WaveSO> _wavesData = new List<WaveSO>();
    [SerializeField] List<WaveHandlerInfo> waveHandlers = new List<WaveHandlerInfo>();

    [Header("WaveUI")]
    [SerializeField] GameObject _waveUI;
    [SerializeField] TMP_Text _timerText;

    [Header("Start")]
    [SerializeField] float _startTime;
    [SerializeField] bool _startGame;

    private List<WaveVariables> _waveData = new List<WaveVariables>();
    private WaveType _waveType;
    private WaveSO _currentWave;

    //Counter Died
    private int _deathsToSpawnNext;
    private int _currentDied;

    private float _timerTillNextSpawn;
    private bool _canSpawnNext;
    //Wave spawn Rate
    private float _waveSpawnRate;
    private float _currentRate;
    //Wave Delay
    private float _currentDelay;
    private float _waveDelay;

    private bool _startWaveSpawn;
    private int _currentIndex;

    List<Enemy> _enemyToActive = new List<Enemy>();
    List<Enemy> _activeEnemy = new List<Enemy>();

    private void Start()
    {
        InitPool();
    }

    private void Update()
    {
        if(_startGame == false)
        {
            StartTimer();
        }

        if (_startWaveSpawn)
        {
            DecideSpawnBehavior();
        }
    }

    #region Create Pools & Orginize enemies
    void InitPool()
    {
        for (int i = 0; i < waveHandlers.Count; i++)
        {
            SetEnemiesPool(waveHandlers[i]);
        }
    }

    //Listing each enemy to it's own group, under the specific holder
    void SetEnemiesPool(WaveHandlerInfo wave)
    {
        for (int i = 0; i < wave.amountOfEnemies; i++)
        {
            Enemy enemy = Instantiate(wave.enemyPreFab, wave.enemyHolder);
            enemy.OnEnemyDied += DealEnemy;
            enemy.gameObject.SetActive(false);
            wave.enemyQueue.Enqueue(enemy);
        }
    }
    #endregion

    //set wave data
    void InitData(int index)
    {
        if (index > _wavesData.Count)
        {
            LevelManager.OnLevelWin.Invoke();
            return;
        }

        _currentRate = 0;
        _currentIndex = index;

        _currentWave = _wavesData[index-1];
        _waveData = _currentWave.WaveData;
        _waveType = _currentWave.WaveType;
        _timerTillNextSpawn = _currentWave.TimerNextWave;
        _deathsToSpawnNext = _currentWave.KillAmount;
        _waveDelay = _currentWave.WaveDelay;
        _currentDelay = _waveDelay;
        _currentDied = 0;

        SetSpawnList();
        _waveSpawnRate = _currentWave.SpawnRate;
        _startWaveSpawn = true;
    }

    #region Wave to Active Enemies
    void SetSpawnList()
    {
        for (int i = 0; i < _waveData.Count; i++)
        {
            CheckEnemyToSpawn(_waveData[i]);
        }
    }

    //check if the SO exist in one of the enemy holders
    //Put the current SO in the active list
    void CheckEnemyToSpawn(WaveVariables wave)
    {
        for (int i = 0; i < waveHandlers.Count; i++)
        {
            string waveDataString = wave.enemydata.EnemyName;
            string enemyString = waveHandlers[i].enemyPreFab.EnemyData.EnemyName;
            if (waveDataString== enemyString)
            {
                int numberToSpawn = wave.enemyNumber;
                SetEnemiesToActiveList(waveHandlers[i], numberToSpawn);
            }
        }
    }

    void SetEnemiesToActiveList(WaveHandlerInfo waveInfo, int amount)
    {
        for (int j = 0; j < amount; j++)
        {
            Enemy enemy = waveInfo.enemyQueue.Dequeue();
            _enemyToActive.Add(enemy);
            waveInfo.enemyQueue.Enqueue(enemy);
        }
    }

    #endregion

    #region Wave Behavior
    //Decide what is the condition of each wave to end
    void DecideSpawnBehavior()
    {
        switch(_waveType)
        {
            case WaveType.KillingAmount:
                 if(_currentDied>= _deathsToSpawnNext)
                    WaveDelayTimer();
                break;
            case WaveType.Timer:
                if (_canSpawnNext)
                    WaveDelayTimer();
                else
                    WaveTimerTypeDelay();
                break;
            case WaveType.Clear:
                if(_activeEnemy.Count == 0)
                    WaveDelayTimer();
                break;
        }
        DefaultWaveBehavior();
    }
    void DefaultWaveBehavior()
    {
        SpawnTimer();
        SetTargets();
    }

    #region Timers

    //When timer is up, the next wave will deploy
    void WaveDelayTimer()
    {
        if (_currentDelay > 0)
        {
            _currentDelay -= Time.deltaTime;
        }
        else
        {
            InitData(_currentIndex + 1);
        }
    }

    //If the next wave can spawn when the time runs out
    void WaveTimerTypeDelay()
    {
        if(_timerTillNextSpawn>0)
        {
            _timerTillNextSpawn -= Time.deltaTime;
            _canSpawnNext = false;
        }
        else
        {
            _timerTillNextSpawn = 0;
            _canSpawnNext = true;
        }
    }

    //On start wave spawn
    void StartTimer()
    {
        _timerText.text = $"{(int)_startTime }";
        if (_startTime > 0)
            _startTime -= Time.deltaTime;
        else
        {
            _waveUI.SetActive(false);
            _startTime = 0;
            _startGame = true;
            InitData(1);
        }
    }

    //The deference between each spawn 
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
    #endregion

    //Spawn Random enemy from the wave list 
    void Spawn()
    {
        if (_enemyToActive.Count == 0)
            return;

        int rndPos = 0;
        if (_enemyToActive.Count > 1)
            rndPos = Random.Range(0, _enemyToActive.Count);
        else
            rndPos = 0;

        Enemy enemy = _enemyToActive[rndPos];
        _enemyToActive.Remove(_enemyToActive[rndPos]);
        enemy.gameObject.SetActive(true);
        _activeEnemy.Add(enemy);
    }

    //Give the Tower the active enemies list
    void SetTargets()
    {
        if (_activeEnemy.Count > 0)
            UpgradeManager.Instance.TorretHandler.CurrentTower.Targets = _activeEnemy;
    }

    #endregion

    //Once enemy is dead, Remove from the active list
    //Count how many died in this wave
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

}
