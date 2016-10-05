using UnityEngine;
using System.Collections;

public class BlockLimiter : MonoBehaviour {
	[Range(1,10)]
	public int maxRaisedAmount = 5;
	private int raised;

	private bool Raisins;

	// Use this for initialization
	void Start () {
		Raisins = true;
		raised = 0;
	}
	
	// Update is called once per frame
	void Update () {
		CheckRaise ();
	}

	public void setRaised(int blockRaised){
		raised += blockRaised;
		Debug.Log ("Raised amount: " + raised);
	}

	void CheckRaise(){
		if (raised >= maxRaisedAmount) {
			Raisins = false;
		}
		if (raised < maxRaisedAmount) {
			Raisins = true;
		}
	}

}
