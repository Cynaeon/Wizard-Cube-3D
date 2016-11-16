using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

	public float speed = 0.25f;
    public float zoomSpeed = 2f;

    private float cameraMinSize = 4;
    private float cameraMaxSize = 6;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);
		transform.position += movement * speed;

        //transform.position = Mathf.Clamp(transform.position.z, -10, 0);

        float fov = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        
        Camera.main.orthographicSize -= fov;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, cameraMinSize, cameraMaxSize);
    }
}
