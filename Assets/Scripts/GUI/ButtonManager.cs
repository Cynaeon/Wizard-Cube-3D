using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace WizardCube
{
    public class ButtonManager : MonoBehaviour
    {

        public void NewGameButton(string level)
        {
			if (Time.timeScale != 1) {
				Time.timeScale = 1;
			}
            GameManager.Instance.AudioManager.playSoundEffect(4);

            GameManager.Instance.LevelEndSettings();

            StateType currentForComparison = GameManager.Instance.StateManager.CurrentStateType;
            //Debug.LogWarning("Current state before transition: " + currentForComparison);

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

            //Debug.LogWarning("Current state after transition: " + GameManager.Instance.StateManager.CurrentStateType);
            SceneManager.LoadSceneAsync(level, LoadSceneMode.Single);
        }

        public void MoveToLevelFromMenu(int buildIndexToGoTo)
        {
            StateType currentForComparison = GameManager.Instance.StateManager.CurrentStateType;
            //Debug.LogWarning("Current state before transition: " + currentForComparison);

            GameManager.Instance.AudioManager.playSoundEffect(4);
            GameManager.Instance.LevelManager.MoveOutOfMenu(buildIndexToGoTo);
            GameManager.Instance.AudioManager.playSoundEffect(13);

            //Debug.LogWarning("Current state after transition: " + GameManager.Instance.StateManager.CurrentStateType);
        }

        public void ChangeLevel(int buildIndexToGoTo)
        {
            GameManager.Instance.LevelManager.MoveToStageX(buildIndexToGoTo);
        }

        public void FastForwardActive()
        {
            GameManager.Instance.fastForward = true;
        }

        public void FastForwardNotActive()
        {
            GameManager.Instance.fastForward = false;
        }

        public void ToMenuButton()
        {
            StateType currentForComparison = GameManager.Instance.StateManager.CurrentStateType;
            //Debug.LogWarning("Current state before transition: " + currentForComparison);

            GameManager.Instance.AudioManager.playSoundEffect(4);
            GameManager.Instance.LevelManager.MoveToLevelSelect();

            if (currentForComparison != StateType.GameOver)
            {
                GameManager.Instance.AudioManager.playSoundEffect(13);
            }
            else if (currentForComparison == StateType.GameOver)
            {
                GameManager.Instance.AudioManager.toggleMusicMute();
            }

            //Debug.LogWarning("Current state after transition: " + GameManager.Instance.StateManager.CurrentStateType);
        }

        public void RetryGameOverButton()
        {
            /*int lastSceneBuildIndex = GameManager.Instance.sceneBeforeGameOver;
            Debug.LogWarning(lastSceneBuildIndex);

            GameManager.Instance.StateManager.PerformTransition(TransitionType.GameOverToPreparations);
            SceneManager.LoadSceneAsync(lastSceneBuildIndex, LoadSceneMode.Single);*/

            GameManager.Instance.AudioManager.playSoundEffect(4);
            GameManager.Instance.LevelManager.GameOverRetry();
            GameManager.Instance.AudioManager.toggleMusicMute();
        }

        public void ExitGameButton()
        {
            GameManager.Instance.AudioManager.playSoundEffect(4);
            Application.Quit();
        }

        public void NextLevelButton()
        {
            GameManager.Instance.AudioManager.playSoundEffect(4);
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
