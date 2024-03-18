using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameObject Instance { get; private set; }
    
    public Vector3 spawnPoint;
    public GameObject playerPrefab;
    public bool isAlive;
    
    public Transform projectilesContainer;

    private GameObject _player;
    
    protected void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = gameObject; 
        }
    }

    protected void Start()
    {
        _player = InstantiatePlayer();
    }

    protected void FixedUpdate()
    {
        if (!isAlive)
        {
            _player = InstantiatePlayer();
        }
    }

    public Vector3 GetPlayerPosition()
    {
        return _player.transform.position;
    }
    
    private GameObject InstantiatePlayer()
    {
        var newPlayer = Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
        isAlive = true;

        return newPlayer;
    }
    
    public void OnDeath()
    {
        isAlive = false;
        MovePlayerToSpawnPoint();
    }

    private void MovePlayerToSpawnPoint()
    {
        _player.transform.position = spawnPoint;
    }
}

