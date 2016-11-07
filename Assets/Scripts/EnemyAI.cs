using UnityEngine;
using System.Collections;

namespace WizardCube
{
    public class EnemyAI : MonoBehaviour {

        [SerializeField]
        private AILerp _aiLerp;

	    // Use this for initialization
	    void Start ()
        {
	        if (_aiLerp == null)
            {
                Debug.LogError("EnemyAI reference for AILerp is null.");
                Debug.Break();
            }
	    }
	
	    // Update is called once per frame
	    void Update ()
        {
            if (transform.position.y <= 0.54f)
            {
                _aiLerp.canMove = false;
                transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
                Debug.Log("One AI fell into a hole, movement stopped.");
            }
	    }

        // Use this when you want to either make this gameobject move or stop moving
        public void MovementControl(bool haltMovement)
        {
            if (haltMovement)
            {
                _aiLerp.canMove = false;
            }
            else if (!haltMovement)
            {
                _aiLerp.canMove = true;
            }
        }

        public void ChangeTarget(Transform newTarget)
        {
            _aiLerp.target = newTarget;
        }
    }
}
