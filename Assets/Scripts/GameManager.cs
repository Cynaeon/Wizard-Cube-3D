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
        private GameObject _treasure;
        private GameObject _beginButton;
        private GameObject _fastForwardButton;

        public bool fastForward;

        public int latestUnlockedLevel;

		public BlockLimiter _blockLimiter { get; private set; }

        public int sceneBeforeGameOver { get; private set; }

        public StateManager StateManager { get; private set; }
        public LevelManager LevelManager { get; private set; }
        public AudioManager AudioManager { get; private set; }

        private void Awake()
        {
            if (_instance == null)
            {
                DontDestroyOnLoad(gameObject);
                _instance = this;
                Initialize();
            }

            if (_instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        private void FixedUpdate()
        {
            // Fast forwarding
            //if (Input.GetButton("Fire1"))
            if (fastForward)
            {
                Time.timeScale = 4.0f;
            }
            else if (!fastForward)
            {
                Time.timeScale = 1.0f;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                DebugUnlockAll();
            }
            else if (Input.GetKeyDown(KeyCode.Y))
            {
                ResetSave();
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
            LevelManager = new LevelManager();
            //_turretArray = FindObjectsOfType(typeof(Turret)) as Turret[];
            //_victoryWindow = GameObject.FindWithTag("VictoryWindow");
            //_victoryWindow.SetActive(false);
            LevelBeginSettings();
            CheckAll();
            LoadGame();
        }

        private void InitializeStateManager ()
        {
            StateManager = new StateManager(new MenuState());
            StateManager.AddState(new PreparationState());
            StateManager.AddState(new ActiveState());
            StateManager.AddState(new GameOverState());
            StateManager.AddState(new VictoryState());
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
            AudioManager = FindObjectOfType<AudioManager>();
            Scene currentScene = SceneManager.GetActiveScene();
            //Debug.LogWarning(currentScene.buildIndex);

            fastForward = false;

            if (currentScene.buildIndex >= 3)
            {
                _turretArray = FindObjectsOfType(typeof(Turret)) as Turret[];
                _turretList = Utilities.ConvertToList<Turret>(_turretArray);

                _enemyArray = FindObjectsOfType(typeof(EnemyAI)) as EnemyAI[];
                _enemyList = Utilities.ConvertToList<EnemyAI>(_enemyArray);

                _victoryWindow = GameObject.FindWithTag("VictoryWindow");
                _victoryWindow.SetActive(false);

                _treasure = GameObject.FindWithTag("Treasure");

                _beginButton = GameObject.Find("BeginButton");

                if (!_beginButton.activeInHierarchy)
                {
                    ToggleBeginButton(true);
                }

                _fastForwardButton = GameObject.Find("FFButton");

                if (_fastForwardButton.activeInHierarchy)
                {
                    ToggleFastForwardButton(false);
                }
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

        public void AddTurretToList()
        {

        }

        public void ControlEnemyMovement(bool haltMovement)
        {
            foreach(EnemyAI enemy in _enemyArray)
            {
                enemy.GetComponent<EnemyAI>().MovementControl(haltMovement);
            }
        }

        public void ControlEnemySearch(bool shouldEnemySearch)
        {
            foreach (EnemyAI enemy in _enemyArray)
            {
                enemy.GetComponent<EnemyAI>().SearchControl(shouldEnemySearch);
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
            }
        }

        public void ToggleVictoryWindow()
        {
            _victoryWindow.SetActive(true);
            AudioManager.playSoundEffect(19);
        }

        public void FireTheTurrets()
        {
            if (_turretList.Count >= 0)
            {
                foreach (Turret tur in _turretList)
                {
						tur.ToggleSafety (true);
                }
            }
        }


        public void MoveToGameOver()
        {
            LevelEndSettings();

            Scene currentScene = SceneManager.GetActiveScene();
            sceneBeforeGameOver = currentScene.buildIndex;

            StateManager.PerformTransition(TransitionType.ActiveToGameOver);
        }

        public void ToggleBeginButton(bool isVisible)
        {
            _beginButton.SetActive(isVisible);
			_blockLimiter = GameObject.Find("BlockController").GetComponent<BlockLimiter>();
			_blockLimiter.ChangeBeginState (isVisible);

        }

        public void ToggleFastForwardButton(bool isVisible)
        {
            _fastForwardButton.SetActive(isVisible);
        }

        public List<EnemyAI> GiveEnemyList()
        {
            return _enemyList;
        }

        public void ForceUpdateEnemyPaths()
        {
            foreach (EnemyAI enemy in _enemyList)
            {
                enemy.ForcePathSearch();
            }
        }

        public void ChangeEnemyTargets()
        {
            foreach (EnemyAI enemy in _enemyList)
            {
                enemy.ChangeTarget(_treasure.transform);
            }
        }

        public void ResetSave()
        {
            PlayerPrefs.SetInt("Latest Unlock", 1);
            latestUnlockedLevel = 1;
            SaveGame();
        }

        public void UnlockNextLevel()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            int buildIndexOfLevelToUnlock = currentScene.buildIndex - 1;

            PlayerPrefs.SetInt("Latest Unlock", buildIndexOfLevelToUnlock);
            latestUnlockedLevel = buildIndexOfLevelToUnlock;
            SaveGame();
        }

        public void DebugUnlockAll()
        {
            int buildIndexOfLevelToUnlock = 13;

            PlayerPrefs.SetInt("Latest Unlock", buildIndexOfLevelToUnlock);
            latestUnlockedLevel = buildIndexOfLevelToUnlock;
            SaveGame();
        }

        public void SaveGame()
        {
            PlayerPrefs.Save();
        }

        public void LoadGame()
        {
            int lastUnlockedLevel = PlayerPrefs.GetInt("Latest Unlock", 1);
            latestUnlockedLevel = lastUnlockedLevel;

            //Sound stuff can go here as well
        }
    }
}
