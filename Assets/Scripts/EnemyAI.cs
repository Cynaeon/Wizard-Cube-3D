using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

namespace WizardCube
{
    public class EnemyAI : MonoBehaviour {

        [SerializeField]
        private AILerp _aiLerp;

        public int health = 5;

        private Vector3 rayOrigin;

        private bool hasDetectedAndStopped;

        [SerializeField]
        private Animator _animator;

        private bool _prepareToDie;

        private void Awake()
        {
            rayOrigin = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
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
	
        void FixedUpdate()
        {
            if (hasDetectedAndStopped)
            {
                hasDetectedAndStopped = false;
                _aiLerp.canMove = true;
            }

            AvoidOtherEnemies();
        }

	    // Update is called once per frame
	    void Update ()
        {
            if (_prepareToDie)
            {
                if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !_animator.IsInTransition(0))
                {
                    GameManager.Instance.ManageEnemyList(this);
                    Destroy(this.gameObject);
                }
            }

            if (health < 1 && !_prepareToDie)
            {
                _animator.SetTrigger("Die");
                /*GameManager.Instance.ManageEnemyList(this);
                Destroy(this.gameObject);*/
                _prepareToDie = true;
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
                _animator.SetTrigger("Walk");
                
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

        public void ForcePathSearch()
        {
            _aiLerp.ForceSearchPath();
        }

        public void AvoidOtherEnemies()
        {
            RaycastHit hit;

            UpdateRayPosition();
            Debug.DrawRay(rayOrigin, transform.forward * 0.5f);

            if (Physics.Raycast(rayOrigin, transform.forward, out hit, 0.5f))
            {
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    Debug.LogWarning(hit.collider.gameObject.name + " detected right in front!");
                    
                    if (_aiLerp.canMove)
                    {
                        _aiLerp.canMove = false;
                        hasDetectedAndStopped = true;
                    }
                }
            }
        }

        private void UpdateRayPosition()
        {
            rayOrigin.x = transform.position.x; 
            rayOrigin.y = transform.position.y + 0.5f;
            rayOrigin.z = transform.position.z;
        }
    }
}
