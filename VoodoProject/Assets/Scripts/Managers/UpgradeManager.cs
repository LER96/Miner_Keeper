using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    [SerializeField] TorretHandler _torretHandler;

    public TorretHandler TorretHandler { get => _torretHandler; set => _torretHandler = value; }

    private void Awake()
    {
        Instance = this;
    }
}
