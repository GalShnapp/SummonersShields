using UnityEngine;

public class wasd_move_cam : MonoBehaviour
{
    public GameObject cam;

    public float moveSpeed;
    public int rotationSpeed;
    
    public ShieldBehaviour shieldBehaviour;
    private ShieldBehaviour _shieldComponent;
    
    protected void Awake()
    {
        _shieldComponent = shieldBehaviour.GetComponent<ShieldBehaviour>();
    }
    
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        var percent = _shieldComponent.weightPenaltyPercent / 100f;
        var newSpeed = moveSpeed * (1 - percent);
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0,0,1) * (rotationSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0,0,-1) * (rotationSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.A)) 
        {
            // Relative movement
            //transform.Translate(new Vector3(-1 * newSpeed,0,0) * Time.deltaTime);
            transform.position =
                new Vector3(transform.position.x - newSpeed, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.D)) 
        {
            //transform.Translate(new Vector3(1 * newSpeed,0,0) * Time.deltaTime);
            transform.position =
                new Vector3(transform.position.x + newSpeed, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.W)) 
        {
            //transform.Translate(new Vector3(0,1 * newSpeed,0) * Time.deltaTime);
            transform.position =
                new Vector3(transform.position.x, transform.position.y + newSpeed, transform.position.z);
        }
        if (Input.GetKey(KeyCode.S)) 
        {
            //transform.Translate(new Vector3(0,-1 * newSpeed,0) * Time.deltaTime);
            transform.position =
                new Vector3(transform.position.x, transform.position.y - newSpeed, transform.position.z);
        }

        cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);
    }
}
