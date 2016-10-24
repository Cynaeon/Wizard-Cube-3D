using UnityEngine;
using System.Collections;

namespace WizardCube
{
    public class GridTestEnemy : Pathfinding
    {
        [SerializeField]
        private Transform _goal;

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                FindPath(transform.position, _goal.position);
            }

            if (Path.Count > 0)
            {
                Move();
            }
        }
    }
}