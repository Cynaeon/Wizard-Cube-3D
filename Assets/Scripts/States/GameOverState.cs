using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace WizardCube
{
    public class GameOverState : StateBase
    {
        public GameOverState() : base()
        {
            State = StateType.GameOver;
            AddTransition(TransitionType.GameOverToPreparations, StateType.Preparations);
            AddTransition(TransitionType.GameOverToMenu, StateType.Menu);
        }

        public override void StateActivated()
        {
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
            GameManager.Instance.AudioManager.toggleMusicMute();
            GameManager.Instance.AudioManager.playSoundEffect(11);
        }
    }
}
