using UnityEngine;
using System.Collections.Generic;
using Pathfinding;

namespace WizardCube
{
    public class BlockBehaviour : MonoBehaviour
    {
        [SerializeField]
        private GameObject _changeThisRenderer;
        [SerializeField]
        private GameObject _changeThisToo;
        private Renderer _rend;
        private Renderer _rend2;
        private Color _defaultColor;
        private Color _hightlightColor;
        private BlockLimiter _blockLimiter;
        private GraphUpdateObject _guo;
        private Animator _animator;
        private Turret _turret;

        private int _groundLayerNumber = 9;
        private int _obstacleLayerNumber = 10;

        public GameObject turret;

        bool blockRaised = false;
        bool turretPlaced = false;

        bool rising = false;
        bool lowering = false;

        void Awake()
        {
            _blockLimiter = GameObject.Find("BlockController").GetComponent<BlockLimiter>();
            _guo = new GraphUpdateObject(GetComponent<Collider>().bounds);
            _guo.updatePhysics = true;
            _animator = GetComponent<Animator>();
            _turret = GetComponentInChildren<Turret>();
        }

        // Use this for initialization
        void Start()
        {
            _rend = _changeThisRenderer.GetComponent<Renderer>();
            _rend2 = _changeThisToo.GetComponent<Renderer>();
            _defaultColor = _rend.material.color;
            _hightlightColor = new Color(_defaultColor.r, _defaultColor.g + 2, _defaultColor.b, _defaultColor.a);
        }

        // Update is called once per frame
        void Update()
        {
            if (blockRaised)
            {
                Vector3 v = transform.position;
                v.y = 1.1f;
                transform.position = Vector3.MoveTowards(transform.position, v, 0.075f);
            }
            else
            {
                Vector3 v = transform.position;
                v.y = 0.5f;
                transform.position = Vector3.MoveTowards(transform.position, v, 0.075f);
            }
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
            GameObject Canvas = GameObject.Find("Canvas");
            Pause pause = Canvas.GetComponent<Pause>();

            if (pause.paused)
            {
                if (!blockRaised)
                {
                    if (_blockLimiter.canRaise)
                    {
                        // Highlight the lowered block
                        _rend.material.color = _hightlightColor;
                        _rend2.material.color = _hightlightColor;

                        if (Input.GetMouseButtonDown(0))
                        {
                            if (transform.position.y == 0.5f)
                            {
                                blockRaised = true;
                                _blockLimiter.setRaised(1);
                                gameObject.layer = _obstacleLayerNumber;
                                AstarPath.active.UpdateGraphs(_guo);
                                gameObject.tag = "RaisedBlock";
                            }
                        }
                        /*
						if (Input.GetMouseButtonDown(0))
						{
							//Raise the block
							Vector3 v = transform.position;
							v.y = 1.1f;
							transform.position = v;
							blockRaised = true;
							_blockLimiter.setRaised(1);
							gameObject.layer = _obstacleLayerNumber;
							AstarPath.active.UpdateGraphs(_guo);
							gameObject.tag = "RaisedBlock";
						}
						*/

                    }
                }
                else if (blockRaised && !_blockLimiter.beginPressed)
                {

                    // Highlight the raised block on mouse over
                    if (!turretPlaced)
                    {
                        _rend.material.color = _hightlightColor;
                        _rend2.material.color = _hightlightColor;
                    }

                    // Lower the block
                    if (Input.GetMouseButtonDown(0) && !turretPlaced)
                    {
                        if (transform.position.y == 1.1f)
                        {
                            blockRaised = false;
                            _blockLimiter.setRaised(-1);
                            gameObject.layer = _groundLayerNumber;
                            //AstarPath.active.UpdateGraphs(_guo);
                            AstarPath.active.Scan();
                            gameObject.tag = "Ground";
                        }
                    }
                    /*
					if (Input.GetMouseButtonDown(0) && !turretPlaced)
					{
						Vector3 v = transform.position;
						v.y = 0.5f;
						transform.position = v;
						blockRaised = false;
						_blockLimiter.setRaised(-1);
						gameObject.layer = _groundLayerNumber;
						//AstarPath.active.UpdateGraphs(_guo);
						AstarPath.active.Scan();
						gameObject.tag = "Ground";
					}
					*/

                    // Create turret at the clicked block's location
                    if (Input.GetMouseButtonDown(1) && !turretPlaced && _blockLimiter._turretsPlaced < _blockLimiter.turretsMax)
                    {
                        /*Vector3 pos = new Vector3 (transform.position.x, transform.position.y + 0.75f, transform.position.z);
						Quaternion rot = new Quaternion (0, 0, 0, 0);
						Instantiate (turret, pos, rot);*/
                        _animator.SetTrigger("TurretOn");
                        turretPlaced = true;
                        _blockLimiter.setTurret(1);
                        Debug.LogWarning("The name of the gameobject is: " + _turret.gameObject.name + " and its parent is named: " + _turret.gameObject.transform.parent.name);
                        _turret.TurretRises();




                    }
                }
            }
        }

        void OnMouseExit()
        {
            _rend.material.color = _defaultColor;
            _rend2.material.color = _defaultColor;
        }
    }
}
