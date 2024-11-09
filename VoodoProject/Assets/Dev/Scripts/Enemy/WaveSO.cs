using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StructHandler;

[CreateAssetMenu(fileName = "WaveSO", menuName = "ScriptableObjects/Wave")]
public class WaveSO : ScriptableObject
{
    
    [SerializeField] List<WaveVariables> waveInfo = new List<WaveVariables>();
    [SerializeField] float _spawnDelay;

    public List<WaveVariables> WaveData => waveInfo;
    public float SpawnRate => _spawnDelay;
}
