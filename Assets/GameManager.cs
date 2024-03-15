using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject Instance { get; private set; }

    public Action respawn;
    public Vector3 spawnPoint;
    public GameObject player;
    
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
        InstantiatePlayer();
    }

    private void InstantiatePlayer()
    {
        Instantiate(player, spawnPoint, Quaternion.identity);
    }

    private void OnDeath()
    {
        Destroy(player);
        InstantiatePlayer();
    }
}

