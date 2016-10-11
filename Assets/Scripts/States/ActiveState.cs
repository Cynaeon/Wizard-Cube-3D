using UnityEngine;

namespace WizardCube
{
    public class ActiveState : StateBase
    {
        public ActiveState() : base()
        {
            State = StateType.Active;
            AddTransition(TransitionType.ActiveToGameOver, StateType.GameOver);
        }

        public override void StateActivated()
        {
            GameManager.Instance.ResumeAgents();
        }
    }
}
