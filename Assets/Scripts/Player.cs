using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 0.02f;

	// Use this for initialization
	void Start () {
		transform.LookAt(target);
	}
	
	public Transform target;

	void Update() {
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);
		transform.position += movement * speed;


	}
}
