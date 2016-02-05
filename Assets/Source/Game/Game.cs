using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Manager;
using Type;
using GamepadInput;

public enum GameState
{
    NONE,
    SPLASHSCREEN,
	MENU,
	LOADING, 
	GAME,
	END,
    TUTO
}

public class Game : MonoBehaviour 
{
	private const	int 			_MAX_PLAYERS	=	2;
    [SerializeField]
	private 	 	MinionColor[] 	_minions_start	=   new MinionColor[0];
    [SerializeField]
    private 		GameState 		_state 			= 	GameState.NONE;
	private 		Timer 			_stateTimer 	= 	new Timer(0f);
    private         Timer           _turnTimer      =   new Timer(0f);
    private         Timer           _endTurnTimer   =   new Timer(0f);
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

    private         int             _numberOfTurn               =   0;
    private         int             _numberOfTurnSinceRespawn   =   0;
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
    [SerializeField]
    public          GameObject      portraitPlayerOne;
    [SerializeField]
    public          GameObject      portraitPlayerTwo;
    public          bool            isPreparingEndTurn  { get; set; }
    public          bool            isEndTurnPaused     { get; set; }
    public          int             numberOfTurnSinceRespawn { get { return _numberOfTurnSinceRespawn; } set{ _numberOfTurnSinceRespawn = value; } }

    [SerializeField]
    public          GameObject      blackScreen;
    [SerializeField]
    public          GameObject      splashScreen;
    [SerializeField]
    public          GameObject      blackVictoryScreen;
    [SerializeField]
    public          GameObject      whiteVictoryScreen;
    [SerializeField]
    public          GameObject      tutoScreen;

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
        //PlayerPrefs.SetInt("splashscreen", 0);
        int needSplashScreen = PlayerPrefs.GetInt("splashscreen", 0);
        if (needSplashScreen == 0)
        {
#if EMILIEN
            SwitchState(GameState.LOADING);
#else
            SwitchState(GameState.SPLASHSCREEN);
#endif
        }
        else
        {
            SwitchState(GameState.LOADING);
        }
    }

	// Set the next player as active
	public void SetNextPlayer() {
		this._currentPlayer.CleanPlayer ();

		if(this.currentPlayer.score >= GameConfiguration.MAX_SCORE) {
			SwitchState (GameState.END);
		}

        if (_currentPlayer.aura != null)
        {
            _currentPlayer.aura.SetActive(false);
        }
        
        this.currentPlayer.portrait.GetComponent<PlayerUI>().FadeOut();
		this._currentPlayer = this.GetNextPlayer();
        this.currentPlayer.portrait.GetComponent<PlayerUI>().FadeIn();

        if (_currentPlayer.aura != null)
        {
            _currentPlayer.aura.SetActive(true);
        }

        ControlManager.instance.Clean();
        this.StartTurn();
    }

    public void StartTurn()
    {
        _numberOfTurn++;
        this._turnTimer.duration = this.defaultDuration - this._currentPlayer.timeMalus;
        this._turnTimer.Start();
        this._currentPlayer.SetAction(true);
        this._currentPlayer.portrait.GetComponent<PlayerUI>().InitTimer();
        ActionLoader.instance.endTurn = false;
    }

    public void PrepareEndTurn()
    {
        isPreparingEndTurn = true;

        _endTurnTimer.duration = 0.5f;
        _endTurnTimer.Start();
    }

    private void EndTurn()
    {
        this._currentPlayer.SetAction(false);
        this._currentPlayer.portrait.GetComponent<PlayerUI>().StopTimer();
        this.SetNextPlayer();

        if (_numberOfTurn % 2 == 0)
        {
            if (Minion.number < GameConfiguration.NUMBER_OF_MINION_TRESHOLD && ++_numberOfTurnSinceRespawn >= GameConfiguration.TURN_BEFORE_RESPAWN)
            {
                int index = mapManager.map.GetRandomAvailableIndex();

                mapManager.map.TransformTile(index, (TileType)Random.Range((int)TileType.MINION_GREEN, (int)TileType.MINION_YELLOW));
            }
        }

        isEndTurnPaused = false;
        isPreparingEndTurn = false;
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

		player.playerIndex = playerIndex;

        TileType    tileType        = playerIndex == 0 ? TileType.START_P1 : TileType.START_P2;
        int         tileIndex       = mapManager.map.GetStartIndex(tileType);
        GameObject  playerElements  = new GameObject();

        player.name = "Player " + (playerIndex + 1);
		player.Teleport (tileIndex, true);

        player.controllerIndex = (GamePad.Index)(playerIndex + 1);
        playerElements.name = player.name + " Elements";

		playerElements.transform.SetParent (this.gameElements.transform);
		player.transform.SetParent (playerElements.transform);

        // Portrait
        switch (playerIndex)
        {
            case 0:
                portraitPlayerOne.GetComponent<PlayerUI>().Initialize(player);
                break;

            case 1:
                portraitPlayerTwo.GetComponent<PlayerUI>().Initialize(player);
                break;
        }
        player.portrait.GetComponent<PlayerUI>().FadeOut();

        foreach (MinionColor color in this._minions_start) {

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

        Minion.number++;

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
            case GameState.SPLASHSCREEN:
                if (_stateTimer.IsFinished() == true)
                {
                    SwitchState(GameState.MENU);
                }
                break;

            case GameState.MENU:
                break;

            case GameState.TUTO:
                if (_stateTimer.IsFinished() == true)
                {
                    SwitchState(GameState.LOADING);
                }
                break;

            case GameState.LOADING:
                if(_stateTimer.IsFinished() == true)
                {
                    SwitchState(GameState.GAME);
                }
                break;

            case GameState.GAME:
                if (isPreparingEndTurn == true)
                {
                    if (_endTurnTimer.IsFinished() == true)
                    {
                        this.EndTurn();
                    }
                }
                else if(_turnTimer.IsFinished() == true)
                {
                    if(ActionLoader.instance.isActive == false && isEndTurnPaused == false)
                    {
                        this.PrepareEndTurn();
                    }
                    else
                    {
                        ActionLoader.instance.endTurn = true;
                    }
                }
                break;

            case GameState.END:
                break;
        }
	}

	public void SwitchState(GameState state)
	{
		_state = state;

        switch(state)
        {
            case GameState.SPLASHSCREEN:
#if DEBUG
        Debug.Log("SPLASHSCREEN");
#endif
                // Hide Victory Screens
                splashScreen.GetComponent<ScreenHelper>().Play(true, 0f);
                blackVictoryScreen.GetComponent<ScreenHelper>().Play(false, 0f);
                whiteVictoryScreen.GetComponent<ScreenHelper>().Play(false, 0f);
                blackScreen.GetComponent<ScreenHelper>().Play(false, 1f);
                _stateTimer.duration = 2f;
                _stateTimer.Start();
                break;

            case GameState.MENU:
#if DEBUG
        Debug.Log("MENU");
#endif
                break;

            case GameState.TUTO:
#if DEBUG
        Debug.Log("TUTO");
#endif
                splashScreen.GetComponent<ScreenHelper>().Play(false, 0f);
                tutoScreen.GetComponent<ScreenHelper>().Play(true, 0f);
                _stateTimer.duration = 5f;
                _stateTimer.Start();
                break;

            case GameState.LOADING:
#if DEBUG
        Debug.Log("LOADING");
#endif
                // Screen stuff
                tutoScreen.GetComponent<ScreenHelper>().Play(false, 0f);
                splashScreen.GetComponent<ScreenHelper>().Play(false, 0f);
                blackScreen.GetComponent<ScreenHelper>().Play(false, 2f);
                _stateTimer.duration = 2f;
                _stateTimer.Start();

                // Initialize the map
                mapManager.StartGame();
                // Initialize all players
                for (int i = 0; i < _MAX_PLAYERS; i++)
                {
                    this.InitializePlayer(i);
                }
                break;


            case GameState.GAME:
#if DEBUG
        Debug.Log("GAME");
#endif
                blackVictoryScreen.GetComponent<ScreenHelper>().Play(false, 0f);
                whiteVictoryScreen.GetComponent<ScreenHelper>().Play(false, 0f);

                // TODO : Make some noise !
                // AudioManager.instance.mainMusic.Play();

                // Set active player
                this._currentPlayer = this.players[1];
                this.SetNextPlayer();
                break;

            case GameState.END:
#if DEBUG
        Debug.Log("END");
#endif
                if(this._currentPlayer == players[0])
                {
                    blackVictoryScreen.GetComponent<ScreenHelper>().Play(true, 0f);
                }
                else
                {
                    whiteVictoryScreen.GetComponent<ScreenHelper>().Play(true, 0f);
                }
                this.currentPlayer.SetAction(false);
                this.GetNextPlayer().SetAction(false);
                break;
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