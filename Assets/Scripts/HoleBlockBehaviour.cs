using UnityEngine;
using System.Collections;

namespace WizardCube
{
    public class HoleBlockBehaviour : MonoBehaviour
    {
        //private BoxCollider _trapCollider;
        private Rigidbody _rigidBody;

        private void Awake()
        {
            //_trapCollider = GetComponentInChildren<BoxCollider>();
            _rigidBody = GetComponentInParent<Rigidbody>();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (transform.position.y <= -4)
            {
                Destroy(transform.parent.gameObject);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyAI>().MovementControl(true);
                other.gameObject.transform.position = new Vector3(2.5f, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
                other.gameObject.transform.SetParent(transform);
                _rigidBody.isKinematic = false;
                _rigidBody.useGravity = true;
            }
        }
    }
}
