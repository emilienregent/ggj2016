using UnityEngine;
using System.Collections;

public class ParticleObject : MonoBehaviour 
{

	[SerializeField]
	private bool 	_isLooping 	= true;
	private Timer 	_timer 		= null;

	// Use this for initialization
	private void Start () 
	{
		_timer = new Timer(1f);
		_timer.Start();

		GetComponent<ParticleSystem>().Play();
	}
	
	// Update is called once per frame
	private void Update () 
	{
		if(_timer.IsFinished() == true && _isLooping == false)
		{
			Destroy(gameObject);
		}
	}
}