﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace WizardCube
{
	public class Tutorial : MonoBehaviour {

		public GameObject _blockController;
		private BlockLimiter _limiter;
		Text text;

		// Use this for initialization
		void Start () {
			_limiter = _blockController.GetComponent<BlockLimiter> ();
			text = GetComponent<Text> ();
		}
		
		// Update is called once per frame
		void Update () {
			
			check ();
		}

		public void beginButtonPressed(){
			text.enabled = false;
		}

		public void check()
		{
			if(Application.loadedLevelName == "level01"){
				if (_limiter._raised == 0) {
					text.text = "Press ground to raise blocks";
				}
				if (_limiter._raised > 0 && _limiter._raised < _limiter.maxRaisedAmount) {
					text.text = "Now make enemies pass over the green blocks";
				}
				if (_limiter._raised == _limiter.maxRaisedAmount) {
					text.text = "Now press begin!";
				}
			}
			if (Application.loadedLevelName == "level05") {
				if (_limiter._turretsPlaced == 0) {
					text.text = "You can place turrets by right clicking a raised cube";
				}
				if (_limiter._turretsPlaced > 0) {
					text.text = "Turrets always target the nearest enemy";
				}
			}
		}


	}

}
