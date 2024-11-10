using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructHandler : MonoBehaviour
{
    [System.Serializable]
    public struct WaveVariables
    {
        public int enemyNumber;
        public Enemy enemyPrefab;
    }
}
