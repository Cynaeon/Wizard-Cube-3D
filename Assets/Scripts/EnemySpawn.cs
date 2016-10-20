using UnityEngine;
using System.Collections;

namespace WizardCube
{
    public class EnemySpawn : MonoBehaviour
    {
        public float spawnRate = 3;
        public int enemyCount = 3;

        public GameObject enemy;

        private float timeStamp;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.Instance.StateManager.CurrentStateType == StateType.Active)
            {
                if (enemyCount > 0 && timeStamp <= Time.time)
                {
                    Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
                    Quaternion rot = new Quaternion(0, 0, 0, 0);
                    Instantiate(enemy, pos, rot);
                    timeStamp = Time.time + spawnRate;
                    enemyCount--;
                }
            }
        }
    }
}