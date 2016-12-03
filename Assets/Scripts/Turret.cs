using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace WizardCube
{
	public class Turret : MonoBehaviour {

		// Firerate in seconds
		public float fireRate = 2;

		public GameObject bullet;

		private float timeStamp;
        private bool _isUp;
        private bool _canFire;

		void Start () {
			timeStamp = Time.time + fireRate;
		}

		void Update () {
            if (_isUp)
            {
                if (_canFire)
                {
                    if (timeStamp <= Time.time)
                    {
                        // Check if there are enemies
                        //GameObject[] gos;
                        //gos = GameObject.FindGameObjectsWithTag("Enemy");
                        List<EnemyAI> _enemyList = GameManager.Instance.GiveEnemyList();
                        Debug.LogWarning(_enemyList.Count);

                        // Create a bullet
                        if (_enemyList.Count > 0)
                        {
                            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.25f, transform.position.z);
                            Quaternion rot = new Quaternion(0, 0, 0, 0);
                            Instantiate(bullet, pos, rot);
                            timeStamp = Time.time + fireRate;
                        }
                    }
                }
            }
		}

        public void TurretRises()
        {
            _isUp = true;
			Debug.LogWarning("isUp: " + _isUp);
        }
		public void TurretLowers()
		{
			_isUp = false;
			Debug.LogWarning ("isUp: " + _isUp);
		}

        public void ToggleSafety(bool shouldTurretFire)
        {
			if (_isUp) { 
				if (shouldTurretFire) { 
					_canFire = true; 
				} 
			}
            else
            {
				if(!shouldTurretFire){
                	_canFire = false;
                //Debug.LogWarning(_canFire);
				}
			}
        }
	}
}