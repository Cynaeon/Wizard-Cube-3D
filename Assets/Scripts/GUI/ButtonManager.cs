﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public void NewGameButton(string level){
		SceneManager.LoadScene(level);
	}

	public void ExitGameButton(){
		Application.Quit();
	}
		
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Escape ();
	}
	public void Escape(){
		if (Application.loadedLevelName == "stageSelect") {
			if (Input.GetKeyDown(KeyCode.Escape)){
				SceneManager.LoadScene("mainMenu");
			}
		}
	}
}