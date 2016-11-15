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

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                GameManager.Instance.MoveToGameOver();
            }
        }
    }
}
