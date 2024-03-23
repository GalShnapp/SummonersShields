using UnityEngine;

public class Gun : MonoBehaviour
{
    public float timeBetweenShots;
    public bool isTargetingPlayer;

    public Projectile projectile;
    public float lifetime;
    public float projectileSpeed;
    public bool isUp;
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
        var myTransform = newProjectile.transform;
        
        //myTransform.position = new Vector3(pos.x, pos.y , pos.z);
        //myTransform.transform.Rotate(new Vector3(transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
        
        newProjectile.Init(lifetime, projectileSpeed, isUp);
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
