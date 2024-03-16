using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance.GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("COLLISION");
        DestroyPlayer();
    }

    private void DestroyPlayer()
    {
        Destroy(_gameManager);
        _gameManager.OnDeath();
    }
}
