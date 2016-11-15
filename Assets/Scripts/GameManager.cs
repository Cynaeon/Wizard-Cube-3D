using UnityEngine;
using System.Collections.Generic;
using System;
using Pathfinding;
using UnityEngine.SceneManagement;

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
        private EnemyAI[] _enemyArray;
        private List<EnemyAI> _enemyList;
        private Turret[] _turretArray;
        private List<Turret> _turretList;

        [SerializeField]
        private GameObject _noControlBlockPrefab;
        private GraphUpdateObject _guo;
        private GameObject _victoryWindow;

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
            //_turretArray = FindObjectsOfType(typeof(Turret)) as Turret[];
            //_victoryWindow = GameObject.FindWithTag("VictoryWindow");
            //_victoryWindow.SetActive(false);
            LevelBeginSettings();
            CheckAll();
        }

        private void InitializeStateManager ()
        {
            StateManager = new StateManager(new PreparationState());
            StateManager.AddState(new ActiveState());
            StateManager.AddState(new GameOverState());
            StateManager.AddState(new VictoryState());
            StateManager.AddState(new MenuState());
        }

        private void HandleStateLoaded(StateType type)
        {
            StateManager.StateLoaded -= HandleStateLoaded;
        }

        // Checks that enemies and NoControlBlockPrefab have been set
        private void CheckAll()
        {
            Scene currentScene = SceneManager.GetActiveScene();

            if (currentScene.buildIndex >= 2) {
                if (_enemyArray.Length <= 0)
                {
                    Debug.LogError("No enemies detected in GameManager Enemies list!");
                    Debug.Break();
                }
            }

            if (_noControlBlockPrefab == null)
            {
                Debug.LogError("NoControlBlockPrefab is null!");
                Debug.Break();
            }
        }
        
        public void LevelBeginSettings()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            Debug.LogWarning(currentScene.buildIndex);

            if (currentScene.buildIndex >= 2)
            {
            _turretArray = FindObjectsOfType(typeof(Turret)) as Turret[];
            _turretList = Utilities.ConvertToList<Turret>(_turretArray);

            _enemyArray = FindObjectsOfType(typeof(EnemyAI)) as EnemyAI[];
            _enemyList = Utilities.ConvertToList<EnemyAI>(_enemyArray);

            _victoryWindow = GameObject.FindWithTag("VictoryWindow");
            _victoryWindow.SetActive(false);
            }
        }

        public void LevelEndSettings()
        {
            if (_turretList != null)
            {
                _turretList.Clear();
            }
            
            if (_enemyList != null)
            {
                _enemyList.Clear();
            }
        }

        public void ResumeEnemies()
        {
            foreach(EnemyAI enemy in _enemyArray)
            {
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

        public void ManageEnemyList(EnemyAI enemyToRemove)
        {
            if (_enemyArray.Length != 0)
            {
                if (_enemyList.Contains(enemyToRemove))
                {
                    _enemyList.Remove(enemyToRemove);
                    CheckVictory();
                }
            }
            
        }

        public void CheckVictory()
        {
            if (_enemyList.Count <= 0)
            {
                //Debug.Log("Victory!");
                //Debug.Break();
                StateManager.PerformTransition(TransitionType.ActiveToVictory);
				Debug.Log ("Kek");
            }
        }

        public void ToggleVictoryWindow()
        {
            _victoryWindow.SetActive(true);
        }

        public void FireTheTurrets()
        {
            if (_turretList.Count <= 0)
            {
                foreach (Turret tur in _turretList)
                {
                    tur.ToggleSafety(true);
                }
            }
        }

        public void MoveToNextStage()
        {
            Scene currentScene = SceneManager.GetActiveScene();

            if (currentScene.buildIndex == 2)
            {
                LevelEndSettings();
                StateManager.PerformTransition(TransitionType.VictoryToPreparations);
                SceneManager.LoadSceneAsync(3, LoadSceneMode.Single);
            }
            else if (currentScene.buildIndex == 3)
            {
                LevelEndSettings();
                StateManager.PerformTransition(TransitionType.VictoryToPreparations);
                SceneManager.LoadSceneAsync(4, LoadSceneMode.Single);
            }
            else
            {
                LevelEndSettings();
                StateManager.PerformTransition(TransitionType.VictoryToPreparations);
                SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
            }
        }
    }
}
