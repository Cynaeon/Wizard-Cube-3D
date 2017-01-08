using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace WizardCube
{
    public class ButtonInfo : MonoBehaviour
    {
        private bool _isUnlocked;
        private Button _thisButton;

        [SerializeField]
        private int _levelId;

        // Use this for initialization
        void Start()
        {
            _thisButton = GetComponent<Button>();
            CheckUnlockStatus();
        }
        
        void CheckUnlockStatus()
        {
            if (GameManager.Instance.latestUnlockedLevel >= _levelId)
            {
                ChangeLockStatus(true);
            }
            else
            {
                ChangeLockStatus(false);
            }
        }

        public void ChangeLockStatus(bool unlock)
        {
            if (unlock)
            {
                _isUnlocked = true;
                _thisButton.interactable = true;
            }
            else
            {
                _isUnlocked = false;
                _thisButton.interactable = false;
            }
        }

        private void OnMouseEnter()
        {
            GameManager.Instance.AudioManager.playSoundEffect(12);       
        }
    }
}
