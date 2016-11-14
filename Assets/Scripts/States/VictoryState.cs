using UnityEngine;
using System.Collections;

namespace WizardCube
{
    public class VictoryState : StateBase
    {
        public VictoryState() : base()
        {
            State = StateType.Victory;
            AddTransition(TransitionType.VictoryToPreparations, StateType.Preparations);
            AddTransition(TransitionType.VictoryToMenu, StateType.Menu);
        }

        public override void StateActivated()
        {
            GameManager.Instance.ToggleVictoryWindow();
            //Debug.Log("Victory!");
            //Debug.Break();
        }
    }
}
