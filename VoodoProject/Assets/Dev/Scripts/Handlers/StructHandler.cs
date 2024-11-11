using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
