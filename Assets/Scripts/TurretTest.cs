using UnityEngine;
using System.Collections;

public class TurretTest : MonoBehaviour {

    private Animator _animator;

	// Use this for initialization
	void Awake () {
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	    //if (Input.GetKeyDown(KeyCode.P))
     //   {
     //       _animator.SetTrigger("TurretOn");
     //   }
	}
}
