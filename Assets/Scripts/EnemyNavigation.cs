using UnityEngine;
using System.Collections;

namespace WizardCube
{
    public class EnemyNavigation : MonoBehaviour
    {
		public int health = 10;

        public Transform goal;
        NavMeshAgent agent;

        private bool _inHole;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.destination = goal.position;
            agent.Stop();
            gameObject.SetActive(false);
        }

        void Update()
        {
            if (transform.position.y <= 1.3f)
            {
                _inHole = true;
                agent.Stop();
                transform.Translate(new Vector3(0, -0.3f, 0) * Time.deltaTime);
            }

            if (health < 1) 
			{
                agent.enabled = false;
				Destroy (this.gameObject);
			}

            if (!agent.hasPath && !_inHole)
            {
                if (agent.isActiveAndEnabled)
                {
                    Debug.Log("Chaser has no path");
                    agent.SetDestination(goal.position);
                    return;
                }
            }
        }

		void OnTriggerEnter(Collider other)
		{
			if (other.tag == "Bullet") 
			{
				health -= 1;
				Destroy (other.gameObject);
			}

		}

        public void MoveOut()
        {
            gameObject.SetActive(true);
            agent.Resume();
        }
    }
}

