using UnityEngine;
using System.Collections;

namespace WizardCube
{
    public class EnemyNavigation : MonoBehaviour
    {
		public int health = 10;

        public Transform goal;
        NavMeshAgent agent;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.destination = goal.position;
        }

        void Update()
        {
			if (health < 1) 
			{
				Destroy (this.gameObject);
			}

            if (!agent.hasPath)
            {
                Debug.Log("Chaser has no path");
                agent.SetDestination(goal.position);
                return;
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
    }
}

