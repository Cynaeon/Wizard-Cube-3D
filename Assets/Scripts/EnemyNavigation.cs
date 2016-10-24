using UnityEngine;
using System.Collections;

namespace WizardCube
{
    public class EnemyNavigation : MonoBehaviour
    {
		public int health = 10;

        public Transform goal;

        private NavMeshAgent _agent;
        private bool _inHole;

        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            
            _agent.Stop();
            //gameObject.SetActive(false);
            //GameObject treasure = GameObject.FindGameObjectWithTag("Defend");
            //goal = treasure.transform;
            _agent.destination = goal.position;
        }

        void Update()
        {
            
            if (transform.position.y <= 1.3f)
            {
                _inHole = true;
                _agent.Stop();
                transform.Translate(new Vector3(0, -0.3f, 0) * Time.deltaTime);
            }

            if (health < 1) 
			{
                _agent.enabled = false;
				Destroy (this.gameObject);
			}

            if (!_agent.hasPath && !_inHole)
            {
                if (_agent.isActiveAndEnabled)
                {
                    Debug.Log("Chaser has no path");
                    _agent.SetDestination(goal.position);
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
            _agent.Resume();
        }

        void OnDrawGizmos()
        {
            if (_agent == null || _agent.path == null)
                return;

            var line = this.GetComponent<LineRenderer>();
            if (line == null)
            {
                line = this.gameObject.AddComponent<LineRenderer>();
                line.material = new Material(Shader.Find("Sprites/Default")) { color = Color.yellow };
                line.SetWidth(0.1f, 0.1f);
                line.SetColors(Color.blue, Color.blue);
            }

            var path = _agent.path;

            line.SetVertexCount(path.corners.Length);

            for (int i = 0; i < path.corners.Length; i++)
            {
                line.SetPosition(i, path.corners[i]);
            }

        }
    }
}

