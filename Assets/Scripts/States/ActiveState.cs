﻿using UnityEngine;

namespace WizardCube
{
    public class ActiveState : StateBase
    {
        public ActiveState() : base()
        {
            State = StateType.Active;
            AddTransition(TransitionType.ActiveToVictory, StateType.Victory);
            AddTransition(TransitionType.ActiveToGameOver, StateType.GameOver);
            AddTransition(TransitionType.ActiveToPreparations, StateType.Preparations);
            AddTransition(TransitionType.ActiveToMenu, StateType.Menu);
        }

        public override void StateActivated()
        {
            GameManager.Instance.ToggleBeginButton(false);
            GameManager.Instance.ControlEnemyMovement(false);
            GameManager.Instance.ControlEnemySearch(false);
            GameManager.Instance.FireTheTurrets();
            GameManager.Instance.AudioManager.TransitionToAction();
            GameManager.Instance.ToggleFastForwardButton(true);
        }
    }
}
