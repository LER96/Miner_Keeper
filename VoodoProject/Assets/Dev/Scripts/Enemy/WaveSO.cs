using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static EnumHandler;
using static StructHandler;

[CreateAssetMenu(fileName = "WaveSO", menuName = "ScriptableObjects/Wave")]
public class WaveSO : ScriptableObject
{
    [SerializeField] List<WaveVariables> waveInfo = new List<WaveVariables>();

    [SerializeField] WaveType waveType;
    private int killAmount;
    private float timerToLaunchNext;
    private float spawnDelay;

    public List<WaveVariables> WaveData => waveInfo;
    public WaveType WaveType => waveType;
    public int KillAmount => killAmount;
    public float TimerNextWave => timerToLaunchNext;
    public float SpawnRate => spawnDelay;

}
