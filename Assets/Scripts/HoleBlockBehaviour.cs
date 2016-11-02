using UnityEngine;
using System.Collections;

namespace WizardCube
{
    public class HoleBlockBehaviour : MonoBehaviour
    {
        //private BoxCollider _trapCollider;

        private void Awake()
        {
            //_trapCollider = GetComponentInChildren<BoxCollider>();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyAI>().MovementControl(true);
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
