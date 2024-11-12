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

    [SerializeField] WaveTypeVariable waveVariable;
    [SerializeField] float spawnDelay;

    public List<WaveVariables> WaveData => waveInfo;
    public WaveType WaveType => waveVariable.waveType;
    public int KillAmount => waveVariable.killAmount;
    public float TimerNextWave => waveVariable.timerToLaunchNext;
    public float SpawnRate => spawnDelay;
    
}
