using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour {

    public float speed = 0.1f;

	// Use this for initialization
	void Start () {
	
	}

    void Update()
    {
        Vector3 temp = Input.mousePosition;
        temp.z = 12.5f; 
        this.transform.position = Camera.main.ScreenToWorldPoint(temp);
        transform.position = new Vector3(transform.position.x, 5f, transform.position.z * 2 - 4.75f);
    }
}
