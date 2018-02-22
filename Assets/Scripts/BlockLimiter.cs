using UnityEngine;
using System.Collections;

namespace WizardCube
{
    public class BlockLimiter : MonoBehaviour
    {
        [Range(1, 20)]
        public int maxRaisedAmount = 5;
        public int _raised;

		public int turretsMax = 3;
		public int _turretsPlaced; 

        public bool canRaise;
        public static BlockLimiter instance;
		public bool beginPressed;

        // Use this for initialization
        void Start()
        {
            canRaise = true;
			beginPressed = false;
            _raised = 0;
            instance = this;
        }

	

        // Update is called once per frame
        void Update()
        {
			if (beginPressed == false) {
				CheckRaise ();
			}
		}


        public void setRaised(int blockRaised)
        {
            _raised += blockRaised;
        }

		public void setTurret(int turretPlaced)
		{
			_turretsPlaced += turretPlaced;
		}

		public void ChangeBeginState(bool isVisible)
		{	
			//if begin button is visible (true) the beginPressed is false (!isVisible) and if begin button is not visible (false) the beginPressed is true (!false)
			beginPressed = !isVisible;
			if (isVisible == false) {
				canRaise = false;
			}
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