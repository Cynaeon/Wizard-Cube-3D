using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
            //GameObject[] gos;
            //gos = GameObject.FindGameObjectsWithTag("Enemy");
            List<EnemyAI> _enemyList = GameManager.Instance.GiveEnemyList();
			GameObject closest = null;
			float distance = Mathf.Infinity;
			Vector3 position = transform.position;
			foreach (EnemyAI enemy in _enemyList) {
				Vector3 diff = enemy.gameObject.transform.position - position;
				float curDistance = diff.sqrMagnitude;
				if (curDistance < distance) {
					closest = enemy.gameObject;
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