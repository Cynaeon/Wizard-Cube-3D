using UnityEngine;

namespace WizardCube
{
    public class ActiveState : StateBase
    {
        public ActiveState() : base()
        {
            State = StateType.Active;
            AddTransition(TransitionType.ActiveToVictory, StateType.Victory);
            AddTransition(TransitionType.ActiveToGameOver, StateType.GameOver);
        }

        public override void StateActivated()
        {
            GameManager.Instance.ResumeEnemies();
        }
    }
}
