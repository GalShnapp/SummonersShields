using UnityEngine;

public class wasd_move_cam : MonoBehaviour
{
    public GameObject cam;

    public int moveSpeed;
    public int rotationSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
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
            transform.Translate(new Vector3(-1,0,0) * (moveSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.D)) 
        {
            transform.Translate(new Vector3(1,0,0) * (moveSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.W)) 
        {
            transform.Translate(new Vector3(0,1,0) * (moveSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.S)) 
        {
            transform.Translate(new Vector3(0,-1,0) * (moveSpeed * Time.deltaTime));
        }

        cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);
    }
}
