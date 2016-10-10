using UnityEngine;
using System.Collections;

namespace WizardCube
{
    public class BlockLimiter : MonoBehaviour
    {
        [Range(1, 10)]
        public int maxRaisedAmount = 5;
        public int _raised;

		public int turretsMax = 3;
		public int _turretsPlaced; 

        public bool canRaise;
        public static BlockLimiter instance;

        // Use this for initialization
        void Start()
        {
            canRaise = true;
            _raised = 0;
            instance = this;
        }

        // Update is called once per frame
        void Update()
        {
            CheckRaise();
        }

        public void setRaised(int blockRaised)
        {
            _raised += blockRaised;
            Debug.Log("Raised amount: " + _raised);
        }

		public void setTurret(int turretPlaced)
		{
			_turretsPlaced += turretPlaced;
		}

        void CheckRaise()
        {
            if (_raised >= maxRaisedAmount)
            {
                canRaise = false;
            }
            if (_raised < maxRaisedAmount)
            {
                canRaise = true;
            }
        }
    }
}