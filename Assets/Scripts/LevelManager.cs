﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace WizardCube
{
    public class LevelManager : MonoBehaviour
    {
        public void MoveToNextStage()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            int currentBuildIndex = currentScene.buildIndex;

            if (currentScene.buildIndex == 11)
            {
                MoveToLevelSelect();
            }
            else
            {
                currentBuildIndex++;

                MoveToStageX(currentBuildIndex);
            }
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
            GameManager.Instance.StateManager.PerformTransition(TransitionType.VictoryToPreparations);
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        }
    }
}

