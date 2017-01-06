using UnityEngine;
using System.Collections.Generic;
using Pathfinding;

namespace WizardCube
{
    public enum moveDirection
    {
        noMovement,
        blockGoingUp,
        blockGoingDown
    }

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
		private Color _hightlightColor2;
        private BlockLimiter _blockLimiter;
        private GraphUpdateObject _guo;
        private Animator _animator;
        private Turret _turret;

        private moveDirection whereDoesBlockGo;
        private bool scanGraph;

        private int _groundLayerNumber = 9;
        private int _obstacleLayerNumber = 10;

        public GameObject turret;

        bool blockRaised = false;
		bool turretPlaced = false;

		bool rising = false;
		bool lowering = false;

        public Texture2D raisedTexture;
        public Texture2D loweredTexture;

        private GameObject canvas;
        private Pause pause;

        void Awake()
        {
            _blockLimiter = GameObject.Find("BlockController").GetComponent<BlockLimiter>();
            _guo = new GraphUpdateObject(GetComponent<Collider>().bounds);
            _guo.updatePhysics = true;
            _animator = GetComponent<Animator>();
            _turret = GetComponentInChildren<Turret>();
            whereDoesBlockGo = moveDirection.noMovement;

            canvas = GameObject.Find("Canvas");
            pause = canvas.GetComponent<Pause>();
        }

        // Use this for initialization
        void Start()
        {
            _rend = _changeThisRenderer.GetComponent<Renderer>();
			_rend2 = _changeThisToo.GetComponent<Renderer>();
            _defaultColor = _rend.material.color;
			_hightlightColor = new Color(_defaultColor.r, _defaultColor.g + 2, _defaultColor.b, _defaultColor.a);
			_hightlightColor2 = new Color(_defaultColor.r+0.5f, _defaultColor.g, _defaultColor.b, _defaultColor.a);
        }

        // Update is called once per frame
        void Update()
        {
			
            if (blockRaised) {
                if (rising)
                {
                    Vector3 v = transform.position;
                    v.y = 1.1f;
                    transform.position = Vector3.MoveTowards(transform.position, v, 0.05f);
                    whereDoesBlockGo = moveDirection.blockGoingUp;
                    _rend.material.mainTexture = raisedTexture;
                    _rend2.material.mainTexture = raisedTexture;
                }
			} else {
                if (lowering)
                {
                    Vector3 v = transform.position;
                    v.y = 0.5f;
                    transform.position = Vector3.MoveTowards(transform.position, v, 0.05f);
                    whereDoesBlockGo = moveDirection.blockGoingDown;
                    _rend.material.mainTexture = loweredTexture;
                    _rend2.material.mainTexture = loweredTexture;
                }
			}
            
            if (whereDoesBlockGo == moveDirection.blockGoingUp && transform.position.y == 1.1f)
            {
                gameObject.layer = _obstacleLayerNumber;
                AstarPath.active.UpdateGraphs(_guo);
                gameObject.tag = "RaisedBlock";
                whereDoesBlockGo = moveDirection.noMovement;
                rising = false;
            }
            else if (whereDoesBlockGo == moveDirection.blockGoingDown && transform.position.y == 0.5f)
            {
                
                gameObject.layer = _groundLayerNumber;
                //AstarPath.active.UpdateGraphs(_guo);
                AstarPath.active.Scan();
                gameObject.tag = "Ground";
                whereDoesBlockGo = moveDirection.noMovement;
                lowering = false;
            } 
            
        }
			
        private void OnMouseEnter()
        {
            if (pause.paused)
            {
                if (GameManager.Instance.StateManager.CurrentStateType == StateType.Preparations)
                {
                    GameManager.Instance.AudioManager.playSoundEffect(12);
                }
            }
        }

        void OnMouseOver()
        {
			if (pause.paused) {

                if (!blockRaised)
	            {
	                if (_blockLimiter.canRaise)
	                {
						// Highlight the lowered block
	                    _rend.material.color = _hightlightColor;
						_rend2.material.color = _hightlightColor;
                    
                       
						if (Input.GetMouseButtonDown(0))
						{
							blockRaised = true;
							_blockLimiter.setRaised(1);
                            rising = true;
                            GameManager.Instance.AudioManager.playSoundEffect(3);
						}
	                }
	            }
				else if (blockRaised && !_blockLimiter.beginPressed)
	            {
					
					// Highlight the raised block on mouse over
					if (!turretPlaced) {
						_rend.material.color = _hightlightColor;
						_rend2.material.color = _hightlightColor;

					}else if(turretPlaced){
						//Debugging
						_rend.material.color = _hightlightColor2;
						_rend2.material.color = _hightlightColor2;
					}

                    
					// Lower the block
					if (Input.GetMouseButtonDown(0) && !turretPlaced)
	                {
	                    blockRaised = false;
	                    _blockLimiter.setRaised(-1);
                        lowering = true;
                        GameManager.Instance.AudioManager.playSoundEffect(0);
                    }

					if (Input.GetMouseButtonDown(1) && turretPlaced)
					{
						turretPlaced = false;
						_blockLimiter.setTurret (-1);
						_turret.TurretLowers ();
						_animator.SetTrigger("TurretOff");
                        GameManager.Instance.AudioManager.playSoundEffect(14);

                    }
                    else if (Input.GetMouseButtonDown (1) && !turretPlaced && _blockLimiter._turretsPlaced < _blockLimiter.turretsMax) 
					{
						
                        _animator.SetTrigger("TurretOn");
                        turretPlaced = true;
						_blockLimiter.setTurret (1);
                        //Debug.LogWarning("The name of the gameobject is: " + _turret.gameObject.name + " and its parent is named: " + _turret.gameObject.transform.parent.name);
                        _turret.TurretRises();
                        GameManager.Instance.AudioManager.playSoundEffect(18);




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
	