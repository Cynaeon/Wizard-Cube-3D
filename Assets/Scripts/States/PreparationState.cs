using UnityEngine;

namespace WizardCube
{
    public class PreparationState : StateBase
    {
        public PreparationState() : base()
        {
            State = StateType.Preparations;
            AddTransition(TransitionType.PreparationsToActive, StateType.Active);
        }

        public override void StateActivated()
        {
            GameManager.Instance.AudioManager.TransitionToPrep();
        }
    }
}
