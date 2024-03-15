using UnityEngine;

public class wasd_move_cam : MonoBehaviour
{
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            Debug.Log("A pressed");
            // camera.Transform.X(x+1)
            // camera.Transform.x = 3;
            Vector3 currPos = cam.transform.position;
            cam.transform.position = new Vector3(currPos.x + 3, currPos.y, currPos.z);
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            Debug.Log("D pressed");
            Vector3 currPos = cam.transform.position;
            cam.transform.position = new Vector3(currPos.x - 3, currPos.y, currPos.z);
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            Debug.Log("W pressed");
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            Debug.Log("S pressed");
        }
    }
}
