using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wasd_move_cam : MonoBehaviour
{

    public GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        camera = GameObject.FindGameObjectsWithTag("MainCamera");
        if (Input.GetKeyDown(KeyCode.A)) {
            Debug.Log("A pressed");
            // camera.Transform.X(x+1)
            camera.transform.
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            Debug.Log("D pressed");
            camera.Transform.x = -3;
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            Debug.Log("W pressed");
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            Debug.Log("S pressed");
        }
    }
}
