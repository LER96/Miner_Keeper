using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    [SerializeField] TorretHandler _torretHandler;
    [SerializeField] TowerUpgradeHandler _towerUpgradeHandler;
    [SerializeField] UpgradeHandler _upgradeHandler;

    public TorretHandler TorretHandler=> _torretHandler;
    public TowerUpgradeHandler TowerUpgradeHandler => _towerUpgradeHandler;
    public UpgradeHandler UpgradeHandler => _upgradeHandler;

    private void Awake()
    {
        Instance = this;
        _torretHandler = GetComponent<TorretHandler>();
        _towerUpgradeHandler = GetComponent<TowerUpgradeHandler>();
        _upgradeHandler = GetComponent<UpgradeHandler>();
    }

    private void Start()
    {
        _towerUpgradeHandler.SetHandler();
        _torretHandler.SetHandler();
        _upgradeHandler.SetHandler();
    }
}
