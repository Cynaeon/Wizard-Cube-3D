using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public AudioMixerSnapshot action;
    public AudioMixerSnapshot prep;

    private float m_TransitionIn = 1;
    private float m_TransitionOut = 2;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        //action.TransitionTo(m_TransitionIn);
        //prep.TransitionTo(m_TransitionOut);
	}

    public void TransitionToAction()
    {
        action.TransitionTo(m_TransitionIn);
    }

    public void TransitionToPrep()
    {
        prep.TransitionTo(m_TransitionOut);
    }
}
