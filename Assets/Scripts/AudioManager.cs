using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

    public AudioMixerSnapshot action;
    public AudioMixerSnapshot prep;
	public AudioMixerSnapshot menu;

    public AudioMixer audioMixer;

    public List<AudioSource> listOfSoundEffects;

    private float m_TransitionAction = 1;
    private float m_TransitionPrep = 2;
	private float m_TransitionMenu = 2;
    private float _volumeBeforeMute;
    private bool _isMuted;

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

    public void toggleMusicMute()
    {
        if (!_isMuted)
        {
            audioMixer.GetFloat("MasterVolume", out _volumeBeforeMute);
            audioMixer.SetFloat("MasterVolume", -80f);
            _isMuted = true;
        } else if (_isMuted)
        {
            audioMixer.SetFloat("MasterVolume", _volumeBeforeMute);
            _isMuted = false;
        }
        
    }

    public void playSoundEffect(int numberOfSfxToPlay)
    {
        if (listOfSoundEffects[numberOfSfxToPlay] != null)
        {
            listOfSoundEffects[numberOfSfxToPlay].Play();
        }
    }

    public void playRandomHitSound()
    {
        int randomNumber = Random.Range(6,7);

        listOfSoundEffects[randomNumber].Play();
    }

    public void playRandomEnemySound()
    {
        int randomNumber = Random.Range(8, 10);

        listOfSoundEffects[randomNumber].Play();
    }

    public void playRandomShootSound()
    {
        int randomNumber = Random.Range(15, 17);

        listOfSoundEffects[randomNumber].Play();
    }
}
