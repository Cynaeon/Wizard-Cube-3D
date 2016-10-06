using UnityEngine;
using System.Collections;

public class BlockLimiter : MonoBehaviour {
	[Range(1,10)]
	public int maxRaisedAmount = 5;
	private int _raised;

	public bool canRaise;

	// Use this for initialization
	void Start () {
		canRaise = true;
		_raised = 0;
	}
	
	// Update is called once per frame
	void Update () {
		CheckRaise ();
	}

	public void setRaised(int blockRaised){
		_raised += blockRaised;
		Debug.Log ("Raised amount: " + _raised);
	}

	void CheckRaise(){
		if (_raised >= maxRaisedAmount) {
			canRaise = false;
		}
		if (_raised < maxRaisedAmount) {
			canRaise = true;
		}
	}

}
