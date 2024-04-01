using UnityEngine;

public class Gun : MonoBehaviour
{
    public float timeBetweenShots;
    public bool isTargetingPlayer;
    public Projectile projectile;
    public float lifetime;
    public float projectileSpeed = 1;
    public float startDelay;
    private float _stopWatchTime;
    
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance.GetComponent<GameManager>();
    }
    
    private void InitProjectile()
    {
        var newProjectile = Instantiate(
            projectile, 
            transform.position, 
            transform.rotation, 
            _gameManager.projectilesContainer
        );
        
        newProjectile.Init(lifetime, projectileSpeed);
    }
    
    void FixedUpdate()
    {
        if (isTargetingPlayer)
        {
            var playerPos = _gameManager.GetPlayerPosition();
            
            Vector2 direction = playerPos - transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        }
        
        _stopWatchTime += Time.deltaTime;
        
        if (_stopWatchTime >= timeBetweenShots)
        {
            InitProjectile();
            _stopWatchTime = 0f;
        }
    }
}
