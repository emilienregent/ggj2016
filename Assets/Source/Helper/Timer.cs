using UnityEngine;
using System.Collections;

public class Timer
{
	private float _time 	= -1f;
	private float _duration = 0f;

	public 	float duration 	{ get { return _duration; } set { _duration = value; } }

	// Use this for initialization
	public Timer (float duration)
	{
		_duration = duration;
	}

	public void Start()
	{
	
		_time = Time.time;
	}

	public void Stop()
	{
		_time = -1;
	}

	public bool IsFinished()
	{
		if(_time != -1 && Time.time - _time > _duration)
		{
			_time = -1;

			return true;
		}

		return false;
	}

	public bool IsRunning()
	{
		if(_time != -1 && Time.time - _time < _duration)
		{
			return true;
		}

		return false;
	}
}

