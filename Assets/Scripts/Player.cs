using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager _gameManager;
    private ShieldBehaviour _shieldBehaviour;
    
    protected void Start()
    {
        _gameManager = GameManager.Instance.GetComponent<GameManager>();
        _shieldBehaviour = GameObject.FindGameObjectWithTag("Shield").GetComponent<ShieldBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("COLLISION");
        if (other.gameObject.CompareTag("Hazard"))
        {
            DestroyPlayer();
        }
        else if (other.gameObject.CompareTag("Shrine"))
        {
            Debug.Log("Shrine");
            _gameManager.MoveSummoner(other.gameObject.transform.position);
        }
    }

    private void DestroyPlayer()
    {
        _gameManager.OnDeath();
    }
}
