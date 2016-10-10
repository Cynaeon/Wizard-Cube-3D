using UnityEngine;
using System.Collections;

namespace WizardCube
{
    public class BlockBehaviour : MonoBehaviour
    {

        private Renderer _rend;
        private Color _defaultColor;
        private Color _hightlightColor;
        private BlockLimiter _blockLimiter;

		public GameObject turret;

        bool blockRaised = false;
		bool turretPlaced = false;

        void Awake()
        {
            _blockLimiter = GameObject.Find("BlockController").GetComponent<BlockLimiter>();
        }

        // Use this for initialization
        void Start()
        {
            _rend = GetComponent<Renderer>();
            _defaultColor = _rend.material.color;
            _hightlightColor = new Color(_defaultColor.r + 50, _defaultColor.b, _defaultColor.b, _defaultColor.a);
        }

        // Update is called once per frame
        void Update()
        {

        }

        //	void OnMouseOver() {
        //		if(GameObject.Find("BlockController").GetComponent<BlockLimiter>().Raisins){
        //		rend.material.color = hightlightColor;
        //
        //			if (Input.GetMouseButtonDown (0)) {
        //				if (!blockRaised) {
        //					Vector3 v = transform.position;
        //					v.y = 1f;
        //					transform.position = v;
        //					blockRaised = true;
        //					GameObject.Find ("BlockController").GetComponent<BlockLimiter> ().setRaised (1);
        //
        //				} else {
        //					Vector3 v = transform.position;
        //					v.y = 0.5f;
        //					transform.position = v;
        //					blockRaised = false;
        //					GameObject.Find ("BlockController").GetComponent<BlockLimiter> ().setRaised (-1);
        //				}
        //			}
        //		}
        //	}

        void OnMouseOver()
        {
            if (!blockRaised)
            {
                if (_blockLimiter.canRaise)
                {
                    _rend.material.color = _hightlightColor;
                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector3 v = transform.position;
                        v.y = 1f;
                        transform.position = v;
                        blockRaised = true;
                        _blockLimiter.setRaised(1);

                    }
                }
            }
            else if (blockRaised)
            {
				
				// Highlight the block on mouse over
				if (!turretPlaced) {
					_rend.material.color = _hightlightColor;
				}

				// Lower the block
				if (Input.GetMouseButtonDown(0) && !turretPlaced)
                {
                    Vector3 v = transform.position;
                    v.y = 0.5f;
                    transform.position = v;
                    blockRaised = false;
                    _blockLimiter.setRaised(-1);
                }

				// Create turret at the clicked block's location
				if (Input.GetMouseButtonDown (1) && !turretPlaced && _blockLimiter._turretsPlaced < _blockLimiter.turretsMax) 
				{
					Vector3 pos = new Vector3 (transform.position.x, transform.position.y + 0.75f, transform.position.z);
					Quaternion rot = new Quaternion (0, 0, 0, 0);
					Instantiate (turret, pos, rot);
					turretPlaced = true;
					_blockLimiter.setTurret (1);
				}
            }
        }

        void OnMouseExit()
        {
            _rend.material.color = _defaultColor;
        }
    }
}