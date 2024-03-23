using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : MonoBehaviour
{
    private GameManager _gameManager;

    // Start is called before the first frame update
    private void Start()
    {
        _gameManager = GameManager.Instance.GetComponent<GameManager>();
        //_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            DestroyPlayer();
        }

        if (other.gameObject.CompareTag("Shrine"))
        {
            Debug.Log("Shrine Activated");
        }
    }
    
    private void DestroyPlayer()
    {
        _gameManager.OnDeath();
    }
}
