using UnityEngine;
using System.Collections;

namespace WizardCube
{
    public class LevelInitialize : MonoBehaviour
    {
        void Awake()
        {
            GameManager.Instance.LevelBeginSettings();
        }
    }
}
