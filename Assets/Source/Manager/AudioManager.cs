using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance = null;

	public AudioMixer audioMixer;
	public AudioMixerSnapshot unPaused;
	public AudioMixerSnapshot paused;
	
	public AudioSource mainMusic = null;

	public AudioSource plop = null;


	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Instanciate(AudioSource audioSource)
	{
		audioSource.PlayOneShot(audioSource.clip);
	}
	
}
