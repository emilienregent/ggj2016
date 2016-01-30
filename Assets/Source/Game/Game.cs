﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Manager;
using Type;
using GamepadInput;

public enum GameState
{
    NONE,
	INTRO, 
	GAME
}

public class Game : MonoBehaviour 
{
	private const	int 			_MAX_PLAYERS	=	2;
    [SerializeField]
	private 	 	MinionColor[] 	_minions_start	=   new MinionColor[0];

	private 		GameState 		_state 			= 	GameState.NONE;
	private 		Timer 			_stateTimer 	= 	new Timer(0f);
    private         Timer           _turnTimer      =   new Timer(0f);
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
    [SerializeField]
    private         GameObject      _managerRoot    = 	null;
    private         Dictionary<ManagerType, IManager>   _managers       = new Dictionary<ManagerType, IManager>();
    private         Dictionary<int, BandOfMinion>       _tileToMinions  = new Dictionary<int, BandOfMinion>();

	private 		GameObject		_gameElements	= 	null;
	public			GameObject		gameElements	{get { return _gameElements; } }

	public static 	Game 	    	instance		= 	null;
	public 			List<Player>	players			= 	new List<Player>();
	public 			GameState 		state 			{ get { return _state; } }
    public          int             mapIndex        { get { return _mapIndex; } }
	public			Player			currentPlayer	{ get { return _currentPlayer; } }
    public          MapManager      mapManager      { get { return (MapManager) _managers[ManagerType.MAP]; } }
    public          Dictionary<int, BandOfMinion> tileToMinions  { get { return _tileToMinions; } set { _tileToMinions = value; } }
    [SerializeField]
    public			float			defaultDuration = 4f;

	private void Awake()
	{
		instance = this;

		this._gameElements = GameObject.Find ("GameElements");

        if (_managerRoot != null)
        {
            foreach(Transform child in _managerRoot.transform)
            {
                IManager manager = child.GetComponent<IManager>();

                if (manager != null)
                {
                    _managers.Add(manager.type, manager);
                }
            }
        }

        foreach(KeyValuePair<ManagerType, IManager> pair in _managers)
        {
            pair.Value.Initialize();
        }

#if UNITY_EDITOR
		Application.targetFrameRate = 60;
#endif
    }

    private void Start()
    {
        // Initialize all players
        for (int i = 0; i < _MAX_PLAYERS; i++)
        {
            this.InitializePlayer(i);
        }

        // Set active player
        this._currentPlayer = this.players[0];

		SwitchState(GameState.INTRO);
    }

	// Set the next player as active
	public void SetNextPlayer() {
		this._currentPlayer = this.GetNextPlayer ();
        ControlManager.instance.Clean();
        this.StartTurn();
    }

    public void StartTurn()
    {
        Debug.Log("Start Turn");
        this._turnTimer.duration = this.defaultDuration - this._currentPlayer.timeMalus;
        this._turnTimer.Start();
        this.currentPlayer.SetAction(true);
    }

    public void EndTurn()
    {
        Debug.Log("End Turn");
        this._currentPlayer.SetAction(false);
        this.SetNextPlayer();
    }

	public Player GetNextPlayer() {
		int playerIndex = this.players.IndexOf(this._currentPlayer);

		if (playerIndex == (this.players.Count - 1))
		{
			return this.players[0];
		}
		else
		{
			return this.players[playerIndex + 1];
		}
	}

	// Initialize one player
	private void InitializePlayer(int playerIndex) {
        
        Player player = Instantiate (this.players[playerIndex]) as Player;

        TileType    tileType        = playerIndex == 0 ? TileType.START_P1 : TileType.START_P2;
        int         tileIndex       = mapManager.map.GetStartIndex(tileType);
        GameObject  playerElements  = new GameObject();

        player.name = "Player " + (playerIndex + 1);
		player.Teleport (tileIndex);

        player.controllerIndex = (GamePad.Index)(playerIndex + 1);
        playerElements.name = player.name + " Elements";

		playerElements.transform.SetParent (this.gameElements.transform);
		player.transform.SetParent (playerElements.transform);
  
		foreach(MinionColor color in this._minions_start) {

            if (player.CanAddMinion () == true) {
                Minion minion = this.CreateMinion(color);
                minion.transform.position = player.transform.position;
                player.AddMinion(minion);
			} else {
#if DEBUG
					Debug.Log("Max minions reached for player " + player.name);
#endif
			}
		}

        this.players[playerIndex] = player;
	}

	// Create a minion
	public Minion CreateMinion(MinionColor color) {
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
			MinionColor randomColor = (MinionColor) Random.Range (1, System.Enum.GetValues(typeof(MinionColor)).Length);
			return this.CreateMinion (randomColor);
		}
	}
	
	// Update is called once per frame
	private void Update () 
	{
        switch(_state)
        {
            case GameState.INTRO:
                if(_stateTimer.IsFinished() == true)
                {
                    SwitchState(GameState.GAME);
                }
                break;

            case GameState.GAME:
                if(_turnTimer.IsFinished() == true)
                {
                    if(ActionLoader.instance.isActive == false)
                    {
                        this.EndTurn();
                    }
                    else
                    {
                        ActionLoader.instance.endTurn = true;
                    }
                }
                break;
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
            this.StartTurn();
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