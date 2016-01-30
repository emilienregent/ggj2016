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
	private const	int 			_MAX_PLAYERS	=	2;
	private 	 	MinionColor[] 	_minions_start	= 	{MinionColor.BLUE, MinionColor.GREEN, MinionColor.RED, MinionColor.YELLOW, MinionColor.GREEN, MinionColor.RED};

	private 		GameState 		_state 			= 	GameState.INTRO;
	private 		Timer 			_stateTimer 	= 	new Timer(0f);
	[SerializeField]
	private 		Player			_currentPlayer 	= 	null;
	[SerializeField]
	private			MinionBlue 		_minionBlue 	= 	null;
	[SerializeField]
	private			MinionGreen		_minionGreen	=	null;
	[SerializeField]
	private			MinionRed		_minionRed		=	null;
	[SerializeField]
	private			MinionYellow	_minionYellow	=	null;
    private         int             _mapIndex       =   0;

	private 		GameObject		_gameElements	= null;
	public			GameObject		gameElements	{get { return _gameElements; } }

	public static 	Game 	    	instance		= 	null;
	public 			List<Player>	players			= 	new List<Player>();
	public 			GameState 		state 			{ get { return _state; } }
    public          int             mapIndex        { get { return _mapIndex; } }
	public			Player			currentPlayer	{ get { return _currentPlayer; } }

	private void Awake()
	{
		instance = this;
		this._gameElements = GameObject.Find ("GameElements");

        SwitchState(GameState.INTRO);

        // Initialize all players
        for (int i = 0; i < _MAX_PLAYERS; i++)
        {
            this.InitializePlayer(i);
        }

        // Set active player
        this._currentPlayer = this.players[0];
        this._currentPlayer.SetAction(true);

#if UNITY_EDITOR
		Application.targetFrameRate = 60;
#endif
    }

	// Set the next player as active
	public void SetNextPlayer() {
		int playerIndex = this.players.IndexOf (this._currentPlayer);

		if(playerIndex == (this.players.Count -1)) {
			this._currentPlayer = this.players[0];
		} else {
			this._currentPlayer = this.players[playerIndex + 1];
		}

		this._currentPlayer.SetAction (true);
	}

	// Use this for initialization
	private void Start () 
	{
	}

	// Initialize one player
	private void InitializePlayer(int playerIndex) {
		this.players [playerIndex] = Instantiate (this.players [playerIndex]) as Player;
		this.players [playerIndex].name = "Player " + (playerIndex + 1);

		GameObject playerElements = new GameObject(this.players [playerIndex].name + " Elements");
		playerElements.transform.SetParent (this.gameElements.transform);
		this.players [playerIndex].transform.SetParent (playerElements.transform);

		foreach(MinionColor color in this._minions_start) {
			if (this.players [playerIndex].CanAddMinion () == true) {
				this.players [playerIndex].AddMinion (this.createMinion (color));
			} else {
				#if DEBUG
					Debug.Log("Max minions reached for player " + this.players [playerIndex].name);
				#endif
			}
		}
	}

	// Create a minion
	private Minion createMinion(MinionColor color) {
		switch(color) {
			case MinionColor.BLUE:
				return Instantiate (this._minionBlue) as MinionBlue;
			case MinionColor.GREEN:
				return Instantiate (this._minionGreen) as MinionGreen;
			case MinionColor.RED:
				return Instantiate (this._minionRed) as MinionRed;
			case MinionColor.YELLOW:
				return Instantiate (this._minionYellow) as MinionYellow;
			default:
				return null;
		}
	}
	
	// Update is called once per frame
	private void Update () 
	{
		if(_stateTimer.IsFinished() == true)
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
			_stateTimer.duration = 2f;
			_stateTimer.Start();
		}
		else if(state == GameState.GAME)
		{
			AudioManager.instance.mainMusic.Play();
		}
	}

	public void Pause()
	{
	}

	public void Resume()
	{
		SwitchState(GameState.GAME);
	}
}