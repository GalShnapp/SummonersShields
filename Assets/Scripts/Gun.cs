using UnityEngine;

public class Gun : MonoBehaviour
{
    public float timeBetweenShots;
    public bool isTargetingPlayer;

    public Projectile projectile;
    public float lifetime;
    public float projectileSpeed;
    
    private float _stopWatchTime;
    
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance.GetComponent<GameManager>();
    }
    
    private void InitProjectile()
    {
        var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        var myTransform = newProjectile.transform;
        var pos = myTransform.position;
        
        myTransform.position = new Vector3(pos.x, pos.y + 0.15f, pos.z);
        newProjectile.Init(lifetime, projectileSpeed);
        newProjectile.FireProjectile(newProjectile.gameObject);
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
