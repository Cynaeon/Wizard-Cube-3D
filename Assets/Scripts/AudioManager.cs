using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

    public AudioMixerSnapshot action;
    public AudioMixerSnapshot prep;
	public AudioMixerSnapshot menu;

    private float m_TransitionAction = 1;
    private float m_TransitionPrep = 2;
	private float m_TransitionMenu = 2;

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
        action.TransitionTo(m_TransitionAction);
    }

    public void TransitionToPrep()
    {
        prep.TransitionTo(m_TransitionPrep);
    }

	public void TransitionToMenu()
	{
		menu.TransitionTo (m_TransitionMenu);

	}

	public void volumeControl(float volumeControl){
		
	}

}
