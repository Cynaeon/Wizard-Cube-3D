using UnityEngine;
using System.Collections;

namespace WizardCube
{

	public class Bullet : MonoBehaviour {

		public Transform target;
		public float speed;

		void Start() {
			GameObject closest = FindClosestEnemy();
			target = closest.transform;
		}

		GameObject FindClosestEnemy() {
			GameObject[] gos;
			gos = GameObject.FindGameObjectsWithTag("Enemy");
			GameObject closest = null;
			float distance = Mathf.Infinity;
			Vector3 position = transform.position;
			foreach (GameObject go in gos) {
				Vector3 diff = go.transform.position - position;
				float curDistance = diff.sqrMagnitude;
				if (curDistance < distance) {
					closest = go;
					distance = curDistance;
				}
			}
			return closest;
		}

		void Update () {
			if (target == null) {
				Destroy (this.gameObject);
				return;
			}
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, target.position, step);
		}
	}
}