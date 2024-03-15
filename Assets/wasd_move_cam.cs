using UnityEngine;

public class wasd_move_cam : MonoBehaviour
{
    public GameObject cam;

    public float moveSpeed = 0.02f;
    public int rotationSpeed = 200;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        var transform1 = transform;
        var position = transform1.position;
        var position1 = cam.transform.position;
        
        position1 = new Vector3(position.x, position.y, position1.z);
        cam.transform.position = position1;

        Vector3 currPos = position1;
        Quaternion currRot = transform1.rotation;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0,0,-1) * (rotationSpeed * Time.deltaTime));
            Debug.Log("<-");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0,0,1) * (rotationSpeed * Time.deltaTime));
            Debug.Log("->");
        }
        if (Input.GetKey(KeyCode.A)) 
        {
            Debug.Log("A pressed");
            cam.transform.position = new Vector3(currPos.x - this.moveSpeed, currPos.y, currPos.z);
        }
        if (Input.GetKey(KeyCode.D)) 
        {
            Debug.Log("D pressed");
            cam.transform.position = new Vector3(currPos.x + this.moveSpeed, currPos.y, currPos.z);
        }
        if (Input.GetKey(KeyCode.W)) 
        {
            Debug.Log("W pressed");
            cam.transform.position = new Vector3(currPos.x , currPos.y + this.moveSpeed, currPos.z);
        }
        if (Input.GetKey(KeyCode.S)) 
        {
            Debug.Log("S pressed");
            cam.transform.position = new Vector3(currPos.x , currPos.y - this.moveSpeed, currPos.z);
        }

        this.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, this.transform.position.z);
    }
}
