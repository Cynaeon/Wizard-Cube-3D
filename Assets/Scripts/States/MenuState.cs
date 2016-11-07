using UnityEngine;
using System.Collections;

namespace WizardCube
{
    public class MenuState : StateBase
    {
        public MenuState() : base()
        {
            State = StateType.Victory;
            AddTransition(TransitionType.VictoryToPreparations, StateType.Preparations);
            AddTransition(TransitionType.VictoryToMenu, StateType.Menu);
        }

        public override void StateActivated()
        {
            
        }
    }
}
