using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

	public GameObject Panel;
	public bool paused;
	// Use this for initialization
	void Awake () {
		Panel.SetActive (false);
		paused = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)){
			ChangePanel ();
		}
	}

	public void ChangePanel () {
		if(Panel.activeInHierarchy == true){
			Panel.SetActive (false);
			Time.timeScale = 1;
			paused = true;
		}else{
			Panel.SetActive (true);
			Time.timeScale = 0;
			paused = false;
		}
		
	}
}
