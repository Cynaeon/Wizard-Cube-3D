using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace WizardCube
{
    public class PreparationGUI : MonoBehaviour
    {
        public void OnBeginButtonPressed ()
        {
            GameManager.Instance.StateManager.PerformTransition(TransitionType.PreparationsToActive);
        }
    }
}
