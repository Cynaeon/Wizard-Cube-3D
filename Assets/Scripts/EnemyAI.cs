using UnityEngine;
using System.Collections;

namespace WizardCube
{
    public class EnemyAI : MonoBehaviour {

        [SerializeField]
        private AILerp _aiLerp;

        public int health = 5;

        void Awake()
        {
            
        }

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
            if (health < 1)
            {
                GameManager.Instance.ManageEnemyList(this);
                Destroy(this.gameObject);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Bullet")
            {
                health -= 1;
                Destroy(other.gameObject);
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

        public void SearchControl(bool shouldEnemySearch)
        {
            if (shouldEnemySearch)
            {
                _aiLerp.canSearch = true;
            }
            else if (!shouldEnemySearch)
            {
                _aiLerp.canSearch = false;
            }
        }

        // Use this for changing this gameobject's target
        public void ChangeTarget(Transform newTarget)
        {
            _aiLerp.target = newTarget;
        }
    }
}
