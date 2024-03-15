using UnityEngine;

public class wasd_move_cam : MonoBehaviour
{
    public GameObject cam;

    private float moveSpeed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currPos = cam.transform.position;
        if (Input.GetKeyDown(KeyCode.A)) {
            Debug.Log("A pressed");
            // camera.Transform.X(x+1)
            // camera.Transform.x = 3
            cam.transform.position = new Vector3(currPos.x - this.moveSpeed, currPos.y, currPos.z);
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            Debug.Log("D pressed");
            cam.transform.position = new Vector3(currPos.x + this.moveSpeed, currPos.y, currPos.z);
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            Debug.Log("W pressed");
            cam.transform.position = new Vector3(currPos.x , currPos.y + this.moveSpeed, currPos.z);
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            Debug.Log("S pressed");
            cam.transform.position = new Vector3(currPos.x , currPos.y - this.moveSpeed, currPos.z);

        }
    }
}
