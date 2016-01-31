using UnityEngine;
using System.Collections;

public class ParticleObject : MonoBehaviour {

	private Timer _timer = null;

	public bool isLooping = true;
	// Use this for initialization
	void Start () {
		_timer = new Timer(1f);
		_timer.Start();
		GetComponent<ParticleSystem>().Play();
	}
	
	// Update is called once per frame
	void Update () {
		if(_timer.IsFinished() && isLooping == false)
		{
			Destroy(gameObject);
		}
	}
}
