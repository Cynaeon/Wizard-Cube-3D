using UnityEngine;
using System.Collections;

namespace WizardCube
{
    public class HoleBlock : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyAI>().ChangeTarget(transform);
            }
        }
    }
}
