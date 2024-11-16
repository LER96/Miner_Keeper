using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    [SerializeField] TorretHandler _torretHandler;
    [SerializeField] TowerUpgradeHandler _upgradeHandler;

    public TorretHandler TorretHandler=> _torretHandler;
    public TowerUpgradeHandler UpgradeHandler => _upgradeHandler;

    private void Awake()
    {
        Instance = this;
        _torretHandler = GetComponent<TorretHandler>();
        _upgradeHandler = GetComponent<TowerUpgradeHandler>();
    }

    private void Start()
    {
        _upgradeHandler.SetHandler();
        _torretHandler.SetHandler();
    }
}
