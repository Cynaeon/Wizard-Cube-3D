﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace WizardCube
{
    public class LevelManager
    {
        public void MoveToNextStage()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            int currentBuildIndex = currentScene.buildIndex;

            if (currentScene.buildIndex >= 15)
            {
                MoveToLevelSelect();
            }
            else
            {
                currentBuildIndex++;

                MoveToStageX(currentBuildIndex);
            }
        }

        public void MoveOutOfMenu(int buildIndexToGoTo)
        {
            GameManager.Instance.LevelEndSettings();
            GameManager.Instance.StateManager.PerformTransition(TransitionType.MenuToPreparations);
            SceneManager.LoadSceneAsync(buildIndexToGoTo, LoadSceneMode.Single);
        }

        public void MoveToStageX(int buildIndexToGoTo)
        {
            GameManager.Instance.LevelEndSettings();

            if (GameManager.Instance.StateManager.CurrentStateType == StateType.Victory)
            {
                GameManager.Instance.StateManager.PerformTransition(TransitionType.VictoryToPreparations);
            }
            else if (GameManager.Instance.StateManager.CurrentStateType == StateType.Active)
            {
                GameManager.Instance.StateManager.PerformTransition(TransitionType.ActiveToPreparations);
            }
            else if (GameManager.Instance.StateManager.CurrentStateType == StateType.Menu)
            {
                GameManager.Instance.StateManager.PerformTransition(TransitionType.MenuToPreparations);
            }

            SceneManager.LoadSceneAsync(buildIndexToGoTo, LoadSceneMode.Single);
        }

        public void MoveToLevelSelect()
        {
            GameManager.Instance.LevelEndSettings();

            if (GameManager.Instance.StateManager.CurrentStateType == StateType.Victory)
            {
                GameManager.Instance.StateManager.PerformTransition(TransitionType.VictoryToMenu);
            }
            else if (GameManager.Instance.StateManager.CurrentStateType == StateType.Active)
            {
                GameManager.Instance.StateManager.PerformTransition(TransitionType.ActiveToMenu);
            }
            else if (GameManager.Instance.StateManager.CurrentStateType == StateType.Preparations)
            {
                GameManager.Instance.StateManager.PerformTransition(TransitionType.PreparationsToMenu);
            }
            else if(GameManager.Instance.StateManager.CurrentStateType == StateType.GameOver)
            {
                GameManager.Instance.StateManager.PerformTransition(TransitionType.GameOverToMenu);
            }

            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        }

        public void GameOverRetry()
        {
            int lastSceneBuildIndex = GameManager.Instance.sceneBeforeGameOver;

            GameManager.Instance.StateManager.PerformTransition(TransitionType.GameOverToPreparations);
            SceneManager.LoadSceneAsync(lastSceneBuildIndex, LoadSceneMode.Single);
        }
    }
}

