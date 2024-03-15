using UnityEngine;

public class wasd_move_cam : MonoBehaviour
{
    public GameObject cam;

    public float moveSpeed = 0.2f;
    public float rotationSpeed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currPos = cam.transform.position;
        Quaternion currRot = this.transform.rotation;
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
