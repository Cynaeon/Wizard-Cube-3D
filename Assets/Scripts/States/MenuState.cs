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
			GameManager.Instance.AudioManager.TransitionToMenu();
        }
    }
}
