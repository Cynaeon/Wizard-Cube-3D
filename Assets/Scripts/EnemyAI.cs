using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    [SerializeField]
    private AILerp _aiLerp;

	// Use this for initialization
	void Start () {
	    if (_aiLerp == null)
        {
            Debug.LogError("EnemyAI reference for AILerp is null.");
            Debug.Break();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y <= 0.54f)
        {
            _aiLerp.canMove = false;
            transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
            Debug.Log("One AI fell into a hole, movement stopped.");
        }
	}
}
