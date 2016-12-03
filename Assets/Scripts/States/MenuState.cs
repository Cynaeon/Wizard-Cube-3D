using UnityEngine;
using System.Collections;

namespace WizardCube
{
    public class MenuState : StateBase
    {
        public MenuState() : base()
        {
            State = StateType.Menu;
            AddTransition(TransitionType.MenuToPreparations, StateType.Preparations);
        }

        public override void StateActivated()
        {
            if (Time.timeScale != 1)
            {
                Time.timeScale = 1;
            }

			GameManager.Instance.AudioManager.TransitionToMenu();
        }
    }
}
