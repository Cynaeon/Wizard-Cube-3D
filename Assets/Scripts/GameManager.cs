using UnityEngine;
using System.Collections.Generic;
using System;

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

        public void ResumeAgents()
        {
            foreach(GameObject enemy in _enemies)
            {
                enemy.GetComponent<EnemyNavigation>().MoveOut();
            }
        }
    }
}
