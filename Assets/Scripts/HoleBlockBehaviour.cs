using UnityEngine;
using System.Collections;

namespace WizardCube
{
    public class HoleBlockBehaviour : MonoBehaviour
    {
        private Rigidbody _rigidBody;
        private float _countdown;
        private bool _timeUntilFall;
        private Vector3 positionAtStart;
        private GameObject enemyInHole;

        private void Awake()
        {
            _rigidBody = GetComponentInParent<Rigidbody>();
            _countdown = 0.5f;
            positionAtStart = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_timeUntilFall)
            {
                if (_countdown <= 0)
                {
                    FallDown();
                }
                else
                {
                    _countdown -= Time.deltaTime;
                }
            }

            if (transform.position.y <= -4)
            {
                GameManager.Instance.AddCube(positionAtStart);
                GameManager.Instance.ManageEnemyList(enemyInHole.GetComponent<EnemyAI>());
                Destroy(transform.parent.gameObject);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                //other.gameObject.GetComponent<EnemyAI>().ChangeTarget(transform);
                //other.gameObject.transform.position = new Vector3(2.5f, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
                other.gameObject.transform.SetParent(transform);
                enemyInHole = other.gameObject;
                _timeUntilFall = true;
                //_rigidBody.isKinematic = false;
                //_rigidBody.useGravity = true;
            }
        }

        void FallDown()
        {
            GameManager.Instance.AudioManager.playSoundEffect(1);
            _rigidBody.isKinematic = false;
            _rigidBody.useGravity = true;
        }
    }
}
