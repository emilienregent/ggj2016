using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public enum GameState
{
	INTRO, 
	GAME
}

public class Game : MonoBehaviour 
{
	private GameState 	_state 		= null;
	private Timer 		_stateTimer = new Timer(0f);

	public static 	Game 	    instance	= null;
	public 			Player 	    player		= null;
	public 			int 	    score 		= 0;
	public 			GameState 	state 		{ get { return _state; } }

	private void Awake()
	{
		instance = this;

#if UNITY_EDITOR
		Application.targetFrameRate = 60;
#endif
	}
		
	// Use this for initialization
	private void Start () 
	{
		SwitchState(GameState.INTRO);
	}
	
	// Update is called once per frame
	private void Update () 
	{
		if(_stateTimer.isFinished() == true)
		{
			if(_state == GameState.INTRO)
			{
				SwitchState(GameState.GAME);
			} 
			else if(_state == GameState.GAME)
			{
				// Do things
			}
		}
	}

	private void SwitchState(GameState state)
	{
		_state = state;

		if(state == GameState.INTRO)
		{
			_stateTimer.SetDuration(2f);
			_stateTimer.Start();
		}
		else if(state == GameState.GAME)
		{
			AudioManager.instance.mainMusic.Play();
		}

		//_stateTimer.SetDuration(0.7f);
		//_stateTimer.Start();
	}

	public void Pause()
	{
	}

	public void Resume()
	{
		SwitchState(GameState.GAME);
	}
}