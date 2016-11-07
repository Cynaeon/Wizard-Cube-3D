using UnityEngine;
using System.Collections.Generic;
using System;
using Pathfinding;

namespace WizardCube
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                }
                return _instance;
            }
        }

        public delegate void SceneLoadedDelegate(int sceneIndex);
        public event SceneLoadedDelegate SceneLoaded;

        [SerializeField]
        private List<GameObject> _enemies;

        [SerializeField]
        private GameObject _noControlBlockPrefab;
        private GraphUpdateObject _guo;

        public StateManager StateManager { get; private set; }
        //...and so on.

        private void Awake()
        {
            if (_instance == null)
            {
                DontDestroyOnLoad(gameObject);
                _instance = this;
                Initialize();
            }
        }

        protected void OnLevelWasLoaded(int levelIndex)
        {
            if (SceneLoaded != null)
            {
                SceneLoaded(levelIndex);
            }
        }

        private void Initialize()
        {
            //Use this for initialization of required GameManager parts.
            InitializeStateManager();
        }

        private void InitializeStateManager ()
        {
            StateManager = new StateManager(new PreparationState());
            StateManager.AddState(new ActiveState());
            StateManager.AddState(new GameOverState());
        }

        private void HandleStateLoaded(StateType type)
        {
            StateManager.StateLoaded -= HandleStateLoaded;
        }
        
        public void ResumeEnemies()
        {
            foreach(GameObject enemy in _enemies)
            {
                //enemy.GetComponent<AILerp>().canMove = true;
                enemy.GetComponent<EnemyAI>().MovementControl(false);
            }
        }

        public void AddCube(Vector3 positionToAdd)
        {
            GameObject placedBlock = Instantiate(_noControlBlockPrefab, positionToAdd, _noControlBlockPrefab.transform.rotation) as GameObject;
            _guo = new GraphUpdateObject(placedBlock.GetComponent<Collider>().bounds);
            _guo.updatePhysics = true;
            AstarPath.active.UpdateGraphs(_guo);
        }
    }
}
