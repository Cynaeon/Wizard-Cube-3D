using UnityEngine;
using System.Collections;

public class BlockBehaviour : MonoBehaviour {

	private Renderer rend;
	private Color defaultColor;
	private Color hightlightColor;

	bool blockRaised = false;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		defaultColor = rend.material.color;
		hightlightColor = new Color (defaultColor.r + 50, defaultColor.b, defaultColor.b, defaultColor.a);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	void OnMouseOver() {
//		if(GameObject.Find("BlockController").GetComponent<BlockLimiter>().Raisins){
//		rend.material.color = hightlightColor;
//
//			if (Input.GetMouseButtonDown (0)) {
//				if (!blockRaised) {
//					Vector3 v = transform.position;
//					v.y = 1f;
//					transform.position = v;
//					blockRaised = true;
//					GameObject.Find ("BlockController").GetComponent<BlockLimiter> ().setRaised (1);
//
//				} else {
//					Vector3 v = transform.position;
//					v.y = 0.5f;
//					transform.position = v;
//					blockRaised = false;
//					GameObject.Find ("BlockController").GetComponent<BlockLimiter> ().setRaised (-1);
//				}
//			}
//		}
//	}

	void OnMouseOver() {
		if (!blockRaised) {
			if (GameObject.Find ("BlockController").GetComponent<BlockLimiter> ().Raisins) {
				rend.material.color = hightlightColor;
				if (Input.GetMouseButtonDown (0)) {
					Vector3 v = transform.position;
					v.y = 1f;
					transform.position = v;
					blockRaised = true;
					GameObject.Find ("BlockController").GetComponent<BlockLimiter> ().setRaised (1);
				
				}
			}
		} else if (blockRaised) {
				rend.material.color = hightlightColor;
				if (Input.GetMouseButtonDown (0)) {
					Vector3 v = transform.position;
					v.y = 0.5f;
					transform.position = v;
					blockRaised = false;
					GameObject.Find ("BlockController").GetComponent<BlockLimiter> ().setRaised (-1);
				}
			}

	}

	void OnMouseExit() {
		rend.material.color = defaultColor; 
	}
}
