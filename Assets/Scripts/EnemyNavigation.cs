using UnityEngine;
using System.Collections;

namespace WizardCube
{
    public class EnemyNavigation : MonoBehaviour
    {
        public Transform goal;
        NavMeshAgent agent;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.destination = goal.position;
        }

        void Update()
        {
            if (!agent.hasPath)
            {
                Debug.Log("Chaser has no path");
                agent.SetDestination(goal.position);
                return;
            }
        }
    }
}

