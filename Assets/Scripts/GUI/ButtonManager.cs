﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace WizardCube
{
    public class ButtonManager : MonoBehaviour
    {

        public void NewGameButton(string level)
        {
            GameManager.Instance.LevelEndSettings();

            StateType currentForComparison = GameManager.Instance.StateManager.CurrentStateType;
            Debug.LogWarning(currentForComparison);

            if (currentForComparison == StateType.Active)
            {
                GameManager.Instance.StateManager.PerformTransition(TransitionType.ActiveToPreparations);
            }
            else if (currentForComparison == StateType.Victory)
            {
                GameManager.Instance.StateManager.PerformTransition(TransitionType.VictoryToPreparations);
            }

            SceneManager.LoadSceneAsync(level, LoadSceneMode.Single);
        }

        public void ExitGameButton()
        {
            Application.Quit();
        }

        public void NextLevelButton()
        {
            GameManager.Instance.MoveToNextStage();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Escape();
        }

        public void Escape()
        {
            if (Application.loadedLevelName == "stageSelect")
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    SceneManager.LoadScene("mainMenu");
                }
            }
        }
    }
}
