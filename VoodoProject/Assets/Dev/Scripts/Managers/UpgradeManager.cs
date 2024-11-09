using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    [SerializeField] TorretHandler _torretHandler;
    [SerializeField] UpgradeHandler _upgradeHandler;

    public TorretHandler TorretHandler=> _torretHandler;
    public UpgradeHandler UpgradeHandler => _upgradeHandler;

    private void Awake()
    {
        Instance = this;
        _torretHandler = GetComponent<TorretHandler>();
        _upgradeHandler = GetComponent<UpgradeHandler>();

        _upgradeHandler.SetHandler();
        _torretHandler.SetHandler();
    }
}
