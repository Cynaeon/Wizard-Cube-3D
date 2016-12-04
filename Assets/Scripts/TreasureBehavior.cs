using UnityEngine;
using System.Collections;

namespace WizardCube
{
    public class TreasureBehavior : MonoBehaviour
    {
        

        private void Awake()
        {
			
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyChild")
            {
                GameManager.Instance.MoveToGameOver();
            }
        }
    }
}
