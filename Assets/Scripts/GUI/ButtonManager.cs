using UnityEngine;
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
            Debug.LogWarning("Current state before transition: " + currentForComparison);

            if (currentForComparison == StateType.Active)
            {
                GameManager.Instance.StateManager.PerformTransition(TransitionType.ActiveToPreparations);
            }
            else if (currentForComparison == StateType.Victory)
            {
                GameManager.Instance.StateManager.PerformTransition(TransitionType.VictoryToPreparations);
            }
            else if (currentForComparison == StateType.GameOver)
            {
                GameManager.Instance.StateManager.PerformTransition(TransitionType.GameOverToPreparations);
            }

            Debug.LogWarning("Current state after transition: " + GameManager.Instance.StateManager.CurrentStateType);
            SceneManager.LoadSceneAsync(level, LoadSceneMode.Single);
        }

        public void RetryGameOverButton()
        {
            /*int lastSceneBuildIndex = GameManager.Instance.sceneBeforeGameOver;
            Debug.LogWarning(lastSceneBuildIndex);

            GameManager.Instance.StateManager.PerformTransition(TransitionType.GameOverToPreparations);
            SceneManager.LoadSceneAsync(lastSceneBuildIndex, LoadSceneMode.Single);*/

            GameManager.Instance.LevelManager.GameOverRetry();
        }

        public void ExitGameButton()
        {
            Application.Quit();
        }

        public void NextLevelButton()
        {
            //GameManager.Instance.MoveToNextStage();
            GameManager.Instance.LevelManager.MoveToNextStage();
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
