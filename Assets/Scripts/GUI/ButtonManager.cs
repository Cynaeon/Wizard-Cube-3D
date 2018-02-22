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
            
            SceneManager.LoadSceneAsync(level, LoadSceneMode.Single);
        }

        public void MoveToLevelFromMenu(int buildIndexToGoTo)
        {
            StateType currentForComparison = GameManager.Instance.StateManager.CurrentStateType;

            GameManager.Instance.AudioManager.playSoundEffect(4);
            GameManager.Instance.LevelManager.MoveOutOfMenu(buildIndexToGoTo);
            
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

            GameManager.Instance.AudioManager.playSoundEffect(4);
            GameManager.Instance.LevelManager.MoveToLevelSelect();

            if (currentForComparison == StateType.GameOver)
            {
                GameManager.Instance.AudioManager.toggleMusicMute();
            }
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

        public void PlayEnterSound(int levelNumber)
        {
            if (GameManager.Instance.latestUnlockedLevel >= levelNumber)
            {
                GameManager.Instance.AudioManager.playSoundEffect(12);
            }
        }

        public void SaveResetButton()
        {
            GameManager.Instance.ResetSave();
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
