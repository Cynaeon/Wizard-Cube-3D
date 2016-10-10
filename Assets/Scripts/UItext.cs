using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace WizardCube {

	public class UItext : MonoBehaviour {

		public Text uiText;
		private BlockLimiter _blockLimiter;

		// Use this for initialization
		void Start () {
			_blockLimiter = GameObject.Find("BlockController").GetComponent<BlockLimiter>();	
		}
		
		// Update is called once per frame
		void Update () {
			uiText.text = "Mana: " + _blockLimiter._raised + " / " + _blockLimiter.maxRaisedAmount
				+ " Turrets: " + _blockLimiter._turretsPlaced + " / " + _blockLimiter.turretsMax;
		}
	}
}