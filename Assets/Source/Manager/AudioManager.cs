using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour 
{

	public static AudioManager instance = null;

	public AudioMixer 			audioMixer	= null;
	public AudioMixerSnapshot 	unPaused	= null;
	public AudioMixerSnapshot 	paused		= null;
	public AudioSource 			mainMusic 	= null;
	public AudioSource 			plop 		= null;

	// Use this for initialization
	private void Start () {
		instance = this;
	}

	public void Instantiate(AudioSource audioSource)
	{
		audioSource.PlayOneShot(audioSource.clip);
	}
}