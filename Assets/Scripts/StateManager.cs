using System.Collections.Generic;

namespace WizardCube
{
    public enum StateType
    {
        Error = -1,
        Menu,
        Preparations,
        Active,
        Victory,
        GameOver
    }

    public enum TransitionType
    {
        Error = -1,
        MenuToPreparations,
        PreparationsToActive,
        PreparationsToMenu,
        ActiveToVictory,
        ActiveToGameOver,
        ActiveToPreparations,
        ActiveToMenu,
        VictoryToPreparations,
        VictoryToMenu,
        GameOverToPreparations,
        GameOverToMenu
    }

    public class StateManager
    {
        public delegate void StateLoadedDelegate(StateType type);
        public event StateLoadedDelegate StateLoaded;

        private List<StateBase> _states = new List<StateBase>();

        public StateBase CurrentState { get; private set; }
        public StateType CurrentStateType { get { return CurrentState.State; } }

        public StateManager(StateBase initialState)
        {
            if (AddState(initialState))
            {
                CurrentState = initialState;
            }
        }

        public void RaiseStateLoaded(StateType state)
        {
            if (StateLoaded != null)
            {
                StateLoaded(state);
            }
        }

        public bool AddState(StateBase state)
        {
            bool exists = false;
            foreach (var stateBase in _states)
            {
                if (stateBase.State == state.State)
                {
                    exists = true;
                }
            }

            if (!exists)
            {
                _states.Add(state);
            }

            return !exists;
        }

        public bool RemoveState(StateType stateType)
        {
            StateBase state = null;
            foreach (var stateBase in _states)
            {
                if (stateBase.State == stateType)
                {
                    state = stateBase;
                }
            }

            return state != null && _states.Remove(state);
        }

        public void PerformTransition(TransitionType transition)
        {
            if (transition == TransitionType.Error)
            {
                return;
            }

            StateType targetStateType = CurrentState.GetTargetStateType(transition);
            if (targetStateType == StateType.Error || targetStateType == CurrentStateType)
            {
                return;
            }

            foreach (var state in _states)
            {
                if (state.State == targetStateType)
                {
                    CurrentState.StateDeactivating();
                    CurrentState = state;
                    CurrentState.StateActivated();
                }
            }
        }
    }
}
