using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace WizardCube {

	public class UILevelInfo : MonoBehaviour {

		public Text levelNumber;
		public Text blocks;
		public Text turrets;
		private BlockLimiter _blockLimiter;

		// Use this for initialization
		void Start () {
			_blockLimiter = GameObject.Find("BlockController").GetComponent<BlockLimiter>();	

			string levelName = Application.loadedLevelName;
			levelName = char.ToUpper(levelName[0]) + levelName.Substring(1);
			levelName = levelName.Insert (5, " ");
			levelNumber.text = levelName;
		}

		// Update is called once per frame
		void Update () {
			int blocksLeft = _blockLimiter.maxRaisedAmount - _blockLimiter._raised;
			blocks.text = "x " + blocksLeft;
			int turretsLeft = _blockLimiter.turretsMax - _blockLimiter._turretsPlaced;
			turrets.text = "x " + turretsLeft;
		}
	}
}