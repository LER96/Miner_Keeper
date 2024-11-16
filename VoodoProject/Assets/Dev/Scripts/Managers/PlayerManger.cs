using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManger : MonoBehaviour
{
    public static PlayerManger Instance;

    [SerializeField] PlayerHandler _playerHandler;

    public float MaxHP=> _playerHandler.MaxHP;
    public float CurrentHP { get=> _playerHandler.CurrentHP; set => _playerHandler.CurrentHP = value; }
    public PlayerHandler PlayerHandler { get => _playerHandler; set => _playerHandler = value; }
    public PlayerMovement MovementScript => _playerHandler.MovementScript;
    public PlayerMining MiningScript => _playerHandler.MiningScript;
    public Inventory PlayerInventory => _playerHandler.PlayerInventory;

    private void Awake()
    {
        Instance = this;
    }
}
