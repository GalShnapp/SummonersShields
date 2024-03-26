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

    void Update()
    {
        PointToPlayer();
    }

    private void PointToPlayer()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
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
