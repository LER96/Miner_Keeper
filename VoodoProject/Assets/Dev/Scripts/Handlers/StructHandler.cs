using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumHandler;

public class StructHandler : MonoBehaviour
{
    [System.Serializable]
    public struct WaveVariables
    {
        public int enemyNumber;
        public EnemySO enemydata;
    }

    [System.Serializable]
    public struct EnemyVariable
    {
        public GameObject EnemyBody;
        public EnemySO EnemyData;
    }

    [System.Serializable]
    public struct WaveTypeVariable
    {
        public WaveType waveType;
        public int killAmount;
        public float timerToLaunchNext;
    }
}
