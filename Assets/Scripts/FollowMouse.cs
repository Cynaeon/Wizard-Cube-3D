using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void Update()
    {
        Vector3 temp = Input.mousePosition;
        temp.z = 12.5f; // Set this to be the distance you want the object to be placed in front of the camera.
        //temp.y = 1.6f;
        this.transform.position = Camera.main.ScreenToWorldPoint(temp);
        transform.position = new Vector3(transform.position.x, 2f, transform.position.z);
    }
}
