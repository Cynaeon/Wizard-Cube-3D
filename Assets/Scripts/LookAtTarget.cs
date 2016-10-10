using UnityEngine;
using System.Collections;

namespace WizardCube
{
	public class LookAtTarget : MonoBehaviour {

		public float speed = 0.02f;
		public Transform target;

		// Use this for initialization
		void Start () {
			transform.LookAt(target);
		}
	}
}
