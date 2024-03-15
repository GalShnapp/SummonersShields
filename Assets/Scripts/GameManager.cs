using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject Instance { get; private set; }

    private List<Collider> _colliders;
    public GameObject colliders;

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

        _colliders = new List<Collider>();
    }

    protected void Start()
    {
        InstantiatePlayer();
        //FindColliders();
    }

    //private void FindColliders()
    //{
    //    throw new NotImplementedException();
    //}

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

