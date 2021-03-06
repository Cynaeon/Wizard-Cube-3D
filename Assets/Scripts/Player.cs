﻿using UnityEngine;
using System.Collections;

namespace WizardCube
{
    public class Player : MonoBehaviour {

	    public float speed = 0.02f;
		public GameObject bulletPrefab;
		public Transform bulletSpawn;

	    // Use this for initialization
	    void Start () {
		    transform.LookAt(target);
	    }

	    public Transform target;

	    void Update() {
			
//			float moveHorizontal = Input.GetAxisRaw ("Horizontal");
//			float moveVertical = Input.GetAxisRaw ("Vertical");
//
//			GameObject Canvas = GameObject.Find ("Canvas");
//			Pause pause = Canvas.GetComponent<Pause> ();
//
//			if (pause.paused) {
//				Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);
//				transform.position += movement * speed;
//			}
		}

	    

	    void OnTriggerEnter(Collider other) {
		    if (other.tag == "Mana") {
			    BlockLimiter.instance.maxRaisedAmount += 1;
			    Destroy (other.gameObject);
		    }
	    }
    }
}
