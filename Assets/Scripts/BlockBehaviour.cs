using UnityEngine;
using System.Collections;
using Pathfinding;

namespace WizardCube
{
    public class BlockBehaviour : MonoBehaviour
    {
		[SerializeField]
		private GameObject _changeThisRenderer;
        private Renderer _rend;
        private Color _defaultColor;
        private Color _hightlightColor;
        private BlockLimiter _blockLimiter;
        private NavMeshObstacle _navMeshObstacle;
        private GraphUpdateObject _guo;
        private Animator _animator;
        private Turret _turret;

        public GameObject turret;

        bool blockRaised = false;
		bool turretPlaced = false;

        void Awake()
        {
            _blockLimiter = GameObject.Find("BlockController").GetComponent<BlockLimiter>();
            //_navMeshObstacle = GetComponent<NavMeshObstacle>();
            _guo = new GraphUpdateObject(GetComponent<Collider>().bounds);
            _guo.updatePhysics = true;
            _animator = GetComponent<Animator>();
            _turret = GetComponentInChildren<Turret>();
        }

        // Use this for initialization
        void Start()
        {
            _rend = _changeThisRenderer.GetComponent<Renderer>();
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
			GameObject Canvas = GameObject.Find ("Canvas");
			Pause pause = Canvas.GetComponent<Pause> ();

			if (pause.paused) {
	            if (!blockRaised)
	            {
	                if (_blockLimiter.canRaise)
	                {
	                    _rend.material.color = _hightlightColor;
	                    if (Input.GetMouseButtonDown(0))
	                    {
	                        Vector3 v = transform.position;
	                        v.y = 1.1f;
	                        transform.position = v;
	                        //_navMeshObstacle.enabled = true;
	                        blockRaised = true;
	                        _blockLimiter.setRaised(1);
	                        AstarPath.active.UpdateGraphs(_guo);
	                    }
	                }
	            }
				else if (blockRaised && !_blockLimiter.beginPressed)
	            {
					
					// Highlight the block on mouse over
					if (!turretPlaced) {
						_rend.material.color = _hightlightColor;
					}

					// Lower the block
					if (Input.GetMouseButtonDown(0) && !turretPlaced)
	                {
	                    //_navMeshObstacle.enabled = false;
	                    Vector3 v = transform.position;
	                    v.y = 0.5f;
	                    transform.position = v;
	                    blockRaised = false;
	                    _blockLimiter.setRaised(-1);
	                    AstarPath.active.UpdateGraphs(_guo);
	                }

					// Create turret at the clicked block's location
					if (Input.GetMouseButtonDown (1) && !turretPlaced && _blockLimiter._turretsPlaced < _blockLimiter.turretsMax) 
					{
                        /*Vector3 pos = new Vector3 (transform.position.x, transform.position.y + 0.75f, transform.position.z);
						Quaternion rot = new Quaternion (0, 0, 0, 0);
						Instantiate (turret, pos, rot);*/
                        _animator.SetTrigger("TurretOn");
                        turretPlaced = true;
						_blockLimiter.setTurret (1);
                        _turret.TurretRises();
					}
	            }
			}
        }

        void OnMouseExit()
        {
            _rend.material.color = _defaultColor;
        }
    }
}     
	