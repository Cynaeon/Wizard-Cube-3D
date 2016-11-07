﻿using UnityEngine;
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
        private Turret[] _turrets;

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
            CheckAll();
            _turrets = FindObjectsOfType(typeof(Turret)) as Turret[];
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

        // Checks that enemies and NoControlBlockPrefab have been set
        private void CheckAll()
        {
            if (_enemies.Count <= 0)
            {
                Debug.LogError("No enemies detected in GameManager Enemies list!");
                Debug.Break();
            }

            if (_noControlBlockPrefab == null)
            {
                Debug.LogError("NoControlBlockPrefab is null!");
                Debug.Break();
            }
        }
        
        public void ResumeEnemies()
        {
            foreach(GameObject enemy in _enemies)
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

        public void ManageEnemyList(GameObject enemyToRemove)
        {
            if (_enemies.Count != 0)
            {
                if (_enemies.Contains(enemyToRemove))
                {
                    _enemies.Remove(enemyToRemove);
                    CheckVictory();
                }
            }
        }

        public void CheckVictory()
        {
            if (_enemies.Count <= 0)
            {
                Debug.Log("Victory!");
                Debug.Break();
            }
        }

        public void FireTheTurrets()
        {
            foreach(Turret tur in _turrets)
            {
                tur.ToggleSafety(true);
            }
        }
    }
}
