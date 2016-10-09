using UnityEngine;
using System.Collections;

namespace WizardCube
{
    public class FloatEffect : MonoBehaviour
    {

        float floatSpan = 0.25f;
        float speed = 1.0f;

        private float startY;

        // Use this for initialization
        void Start()
        {
            startY = transform.position.y;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 tempPos = transform.position;
            tempPos.y = startY + Mathf.Sin(Time.time * speed) * floatSpan / 2.0f;
            transform.position = tempPos;
        }
    }
}
