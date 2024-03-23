using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameObject Instance { get; private set; }
    
    public Vector3 spawnPoint;
    public GameObject playerPrefab;
    public GameObject summonerPrefab;
    public bool isAlive;
    private bool needMoveSum = false;
    public float summonerSpeed;
    public Transform projectilesContainer;
    

    private float startTime;
    private float journeyLength;
    private Vector3 startingSumPos;
    private Vector3 endSumPos; 
    private GameObject _player;
    private GameObject _summoner;
    
    // Move to the target end position.
    void Update()
    {
        if (needMoveSum)
        {
            
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * summonerSpeed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            _summoner.transform.position = Vector3.Lerp(startingSumPos, endSumPos, fractionOfJourney);
            
            if (startingSumPos == endSumPos)
            {
                needMoveSum = false;
            }
        }
    }
    
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
        _summoner = InstantiateSummoner();
        Debug.Log("new ummonetw");
    }

    protected void FixedUpdate()
    {
        if (!isAlive)
        {
            _player = InstantiatePlayer();
            _summoner = InstantiateSummoner();
        }
    }

    public Vector3 GetPlayerPosition()
    {
        return _player.transform.position;
    }

    public void MoveSummoner(Vector3 shrineLocation)
    {
        needMoveSum = true;
        startingSumPos = _summoner.transform.position;
        endSumPos = shrineLocation;
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startingSumPos, endSumPos);
        Debug.Log(journeyLength);
    }
    
    private GameObject InstantiatePlayer()
    {
        var newPlayer = Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
        isAlive = true;

        return newPlayer;
    }
    
    private GameObject InstantiateSummoner()
    {
        var newSummoner = Instantiate(summonerPrefab, spawnPoint, Quaternion.identity);
        isAlive = true;

        return newSummoner;
    }
    
    public void OnDeath()
    {
        SceneManager.LoadScene("myScene 1");
    }
}

