using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Type;
using GamepadInput;

public class Player : MonoBehaviour
{
	// Constantes
	[SerializeField]
	private const 	int 			_MAX_MINIONS 	    = 	6;

	// Privates attributes
	[SerializeField]
	private 		int 			_speed 			    = 	1;
	[SerializeField]
	private 		int 			_countActions 	    = 	1;
	[SerializeField]
	private 		List<Minion> 	_minions 		    = 	new List<Minion>();
	[SerializeField]
	private 		int 			_score 						= 	0;
	[SerializeField]
    private			int			    _tileIndex 		= 	0;
	[SerializeField]
	private 		Animator		_animator		= null;
	[SerializeField]
	private 		GameObject		_dizzyStarsPrefab		= null;
	[SerializeField]
	private 		GameObject		_timerFxPrefab			= null;
	[SerializeField]
	private 		GameObject 		_speedBoostFxPrefab 	= null;
	[SerializeField]
	private 		GameObject 		_topFxRoot 				= null;

	private			GameObject 		_currentTopFxGO 		= null;
    private         GameObject      _aura                   = null;
    private         Tile            _destination            = null;


	private			bool			_canAction		= 	false;
    private         GamePad.Index   _controllerIndex = GamePad.Index.One;
//	private			bool			_canWalkThroughObstacle 	= 	false;
	private			float			_timeMalus		=	0f;
	private			int				_speedBonus		=	0;
	private			int				_invertedControlLeft = 0;
	private 		bool 			_isMoving 		= false;
	private 		Vector3			_targetPosition = Vector3.zero;
	private 		float			_moveSpeed 		= 10f;
	private			float			_rotationSpeed 	= 25f;
	private			Quaternion		_targetRotation = Quaternion.identity;
	private 		bool 			_isTurning		= false;
	private 		int				_playerIndex	= 0;
	private			int				_timeMalusLeft	= 0;

    public          GamePad.Index   controllerIndex { get { return _controllerIndex; } set { _controllerIndex = value; } }
    public			int			    tileIndex		{ get { return _tileIndex; } set { 
            _tileIndex = value; 
        } }

    public			int				score			{ get { return _score; } set { _score = value; } }
	public			bool			canAction		{ get { return _canAction; } }
	public			int				speed			{ get { return _speed; } set { _speed = value; } }
	public			int				speedBonus		{ get { return _speedBonus; } set { _speedBonus = value; } }
	public			float			timeMalus		{ get { return _timeMalus; } set { _timeMalus = value; } }
	public			int				timeMalusLeft	{ get { return _timeMalusLeft; } set { _timeMalusLeft = value; } }
	public			int				invertedControlLeft	{ get { return _invertedControlLeft; } set { _invertedControlLeft = value; } }
	public			List<Minion>	minions			{ get { return _minions; } set { _minions = value; } }
    public          PlayerUI        portrait;
	public 			int 			playerIndex      { get { return _playerIndex; } set { _playerIndex = value; } }
    public          GameObject      aura            { get { return _aura; } }

    private void Awake()
    {
        Transform auraTransform = gameObject.transform.FindChild("Aura");

        if (auraTransform != null)
        {
            _aura = auraTransform.gameObject;
            _aura.SetActive(false);
        }
    }

    // Use this for initialization
    private void Start ()
	{
		#if DEBUG
			Debug.Log ("New player (" + this.name + ") created with " + this._countActions + " action(s) remaining.");
		#endif
	}

	public void Update()
	{
		if (_isMoving == true) 
		{
			Moving ();
		}
		if (_isTurning == true) 
		{
			Turning ();
		}
	}

	// Add a minion to the player
    public void AddMinion(Minion minion, bool teleport = true) {
		#if DEBUG
				Debug.Log ("One minion with color " + minion.color + " added to the player");
		#endif
        minion.name = "Minion " + minion.color + " " + (this._minions.Count+1);
        minion.anchor = this.transform;
		minion.transform.SetParent (this.transform.parent.transform);

		minion.setMinionOffset (this.GetCountMinions (MinionColor.ANY));

        Vector3 position = this.transform.position + Game.instance.mapManager.map.tiles [this.tileIndex].size / 2.5f * minion.offsetPosition;;

        if (teleport == true)
        {
            minion.transform.position = position;
        }

        minion.targetPosition = position;

		this._minions.Add (minion);
	}

	// Remove a minion from the player
	public void RemoveMinion(MinionColor color) {
		if (this._minions.Count > 0) {
			Minion minionToRemove = null;
			foreach (Minion minion in this._minions) {
				if (minion.color == color) {
					minionToRemove = minion;
					break;
				}
			}
			if (minionToRemove != null) {
				this._minions.Remove (minionToRemove);
				for(int i = 0; i < this.minions.Count; i++) {
					minions[i].setMinionOffset (i);
					minions[i].transform.position = this.transform.position + Game.instance.mapManager.map.tiles [this.tileIndex].size / 2.5f * minions[i].offsetPosition;
					minions[i].targetPosition = minions[i].transform.position;
				}
				#if DEBUG
					Debug.Log ("One minion with color " + minionToRemove.color + " has been removed");
				#endif
			} else {
				#if DEBUG
					Debug.Log ("ERROR NO MINION WITH COLOR " + color + " FOUND !");
				#endif
			}
		} else {
			#if DEBUG
				Debug.Log ("The player hasn't any minions.");
			#endif
		}
	}

	public bool CanAddMinion() {
		return this._minions.Count < _MAX_MINIONS;
	}

	// Set if the player can do an action or not
	public void SetAction(bool canAction) {
		this._canAction = canAction;
    }

	// Sacrifice a minion to get a bonus/special action
	public void SacrificeMinion(MinionColor color) {
		if(this.canAction == true && this.GetCountMinions (color) > 0) {
            // Todo : A décommenter
			Minion minion = this.GetMinion(color);
            if (minion.canSacrifice == true)
            {
                Game.instance.numberOfTurnSinceRespawn = 0;
                this.RemoveMinion(color);
                minion.Sacrifice();
                portrait.GetComponent<PlayerUI>().FillMinionSlots(this.minions);
            }
            else
            {
                Game.instance.PrepareEndTurn();
            }
		}
	}

	// Kill one minion, without trigger his effect
	public void KillMinion(MinionColor color) {
		Minion minion = this.GetMinion (color);
		this._minions.Remove (minion);
		minion.Kill ();
        portrait.GetComponent<PlayerUI>().FillMinionSlots(this.minions);
    }

	public void KillAllMinions() {
		Minion minion = null;
        PlayerUI ui = portrait.GetComponent<PlayerUI>();
		while(this.GetCountMinions (MinionColor.ANY) > 0) {
			minion = this._minions [0];
			this.RemoveMinion (minion.color);
			minion.Kill ();
            ui.FillMinionSlots(this.minions);
        }
	}

	// Return one minion of the selected color
	public Minion GetMinion(MinionColor color) {
		switch(color) {
			case MinionColor.BLUE:
				return this.GetOneMinionBlue();
		    case MinionColor.GREEN:
				return this.GetOneMinionGreen ();
			case MinionColor.RED:
				return this.GetOneMinionRed();
			case MinionColor.YELLOW:
				return this.GetOneMinionYellow();
			default:
				return this.GetRandomMinion();
		}
	}

	// Return one blue minion
	private Minion GetOneMinionBlue() {
		foreach(Minion minion in this._minions) {
			if(minion.color == MinionColor.BLUE) {
				return minion;
			}
		}

		return null;
	}

	// Return one green minion
	private Minion GetOneMinionGreen() {
		foreach(Minion minion in this._minions) {
			if(minion.color == MinionColor.GREEN) {
				return minion;
			}
		}

		return null;
	}

	// Return one red minion
	private Minion GetOneMinionRed() {
		foreach(Minion minion in this._minions) {
			if(minion.color == MinionColor.RED) {
				return minion;
			}
		}

		return null;
	}

	// Return one yellow minion
	private Minion GetOneMinionYellow() {
		foreach(Minion minion in this._minions) {
			if(minion.color == MinionColor.YELLOW) {
				return minion;
			}
		}

		return null;
	}

	// Return one random minion
	private Minion GetRandomMinion() {
		if (this.GetCountMinions (MinionColor.ANY) > 0) {
			int randomIndex = Random.Range (0, this.GetCountMinions (MinionColor.ANY));
			return this._minions [randomIndex];
		} else {
			#if DEBUG
			Debug.Log ("ERROR PLAYER " + this.name + " DOESN'T HAVE MINIONS");
			#endif
			return null;
		}
	}

	public List<Minion> GetMinions(MinionColor color) {
		if(color == MinionColor.ANY) {
			return this.minions;
		} else {
			List<Minion> minions = new List<Minion> ();
			foreach(Minion minion in this._minions) {
				if(minion.color == color) {
					minions.Add (minion);
				}
			}
			return minions;
		}
	}

	// Return the total of minions of the player
	public int GetCountMinions(MinionColor color) {
		switch(color) {
			case MinionColor.BLUE:
				return this.GetCountMinionsBlue ();
			case MinionColor.GREEN:
				return this.GetCountMinionsGreen ();
			case MinionColor.RED:
				return this.GetCountMinionsRed ();
			case MinionColor.YELLOW:
				return this.GetCountMinionsYellow ();
			default :
				return this._minions.Count;
		}
	}

	// Return the total of blue minions
	private int GetCountMinionsBlue() {
		int totalMinionBlue = 0;
		foreach(Minion minion in this._minions) {
			if(minion.color == MinionColor.BLUE) {
				totalMinionBlue++;
			}
		}

		return totalMinionBlue;
	}

	// Return the total of green minions
	private int GetCountMinionsGreen() {
		int totalMinionGreen = 0;
		foreach(Minion minion in this._minions) {
			if(minion.color == MinionColor.GREEN) {
				totalMinionGreen++;
			}
		}

		return totalMinionGreen;
	}

	// Return the total of red minions
	private int GetCountMinionsRed() {
		int totalMinionRed = 0;
		foreach(Minion minion in this._minions) {
			if(minion.color == MinionColor.RED) {
				totalMinionRed++;
			}
		}

		return totalMinionRed;
	}

	// Return the total of yellow minions
	private int GetCountMinionsYellow() {
		int totalMinionYellow = 0;
		foreach(Minion minion in this._minions) {
			if(minion.color == MinionColor.YELLOW) {
				totalMinionYellow++;
			}
		}

		return totalMinionYellow;
	}

	public void Release ()
	{
	}

	public void ResetMalusPlayer() {
		this._speed = 1;
		this._speedBonus = 0;
		this._timeMalusLeft = 0;
		this._invertedControlLeft = 0;
		RemoveTopFX ();
	}

	public void CleanPlayer() {
		this._speed = 1;
		this._canAction = false;

		if(this.speedBonus > 0) {
			this.speed = this.speedBonus;
			this.speedBonus = 0;
			RemoveTopFX ();
		}

		if(this._timeMalusLeft > 0) {
			this._timeMalusLeft--;
			if(this._timeMalusLeft == 0) {
				this.timeMalus = 0;
				RemoveTopFX ();
			}
		}

		if(this.invertedControlLeft > 0) {
			this.invertedControlLeft--;
			if (this.invertedControlLeft > 0) {
				this.speed *= -1;
			} else {
				RemoveTopFX ();
			}
		}

	}

    public bool CanMove(int newTileIndex)
    {
        if (newTileIndex < 0 || newTileIndex > Game.instance.mapManager.map.tiles.Count - 1)
        {
            return false;
        }

        for (int i = 0; i < Game.instance.players.Count; i++)
        {
            if (Game.instance.players[i].tileIndex == newTileIndex)
            {
                return false;
            }
        }

        if (Game.instance.mapManager.map.tiles[newTileIndex].type >= (int)TileType.OBSTACLE_FOREST_1)
        {
            return false;
        }

        // Horizontal move ?
        int delta = newTileIndex - _tileIndex;
        if (Mathf.Abs(delta) == _speed)
        {
            // Same line or not
            if(delta > 0)
            {
                return (newTileIndex % Game.instance.mapManager.map.columns > _tileIndex % Game.instance.mapManager.map.columns);
            }
            if(delta < 0)
            {
                return (newTileIndex % Game.instance.mapManager.map.columns < _tileIndex % Game.instance.mapManager.map.columns);
            }
            return false;
            
        } else
        {
            // Same column or not
            return (newTileIndex % Game.instance.mapManager.map.columns == _tileIndex % Game.instance.mapManager.map.columns);
        }

    }

	public void UseMinionGreen()
	{
		if (this.GetCountMinionsGreen () > 0) {
			this.SacrificeMinion (MinionColor.GREEN);
		}
	}

	public void UseMinionBlue()
	{
		if (this.GetCountMinionsBlue () > 0) {
			this.SacrificeMinion (MinionColor.BLUE);
		}
	}

	public void UseMinionYellow()
	{
		if (this.GetCountMinionsYellow () > 0) {
			this.SacrificeMinion (MinionColor.YELLOW);
		}
	}

	public void UseMinionRed()
	{
		if (this.GetCountMinionsRed () > 0) {
			this.SacrificeMinion (MinionColor.RED);
		}
	}

	public void Move(int newTileIndex)
	{
		if (this.canAction == true)
		{
			tileIndex = newTileIndex;

			Tile newTile = Game.instance.mapManager.map.tiles[tileIndex];

            MoveTo(newTile);
		}
	}
		
	private void MoveTo(Tile newTile)
	{

		_animator.SetTrigger ("GoThere");

		_isTurning = true;
        _destination = newTile;

		_targetPosition = newTile.gameObject.transform.position;

		_targetRotation = Quaternion.LookRotation ( newTile.gameObject.transform.position - gameObject.transform.position, Vector3.up);
	}

	private void Turning()
	{
		transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);

		float angle = Quaternion.Angle(transform.rotation, _targetRotation);

		if (Mathf.Approximately(angle, 0f) == true)
		{
			_isTurning = false;
			_isMoving = true;
		}
	}

	private void Moving()
	{
		transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);

		float distance = Vector3.Distance(transform.position, _targetPosition);

		if (Mathf.Approximately(distance, 0f) == true)
		{
			_isMoving = false;

            if (_destination != null)
            {
                EndMovement();
            }
		}
	}

    private void EndMovement()
    {
        _destination.ApplyOnPlayer(this);

        _destination = null;

        if (Game.instance.isEndTurnPaused == false)
        {
            Game.instance.PrepareEndTurn();
        }
    }

    public void Teleport(int tileIndex, bool isInitialization = false)
	{
		_tileIndex = tileIndex;

        _destination = Game.instance.mapManager.map.tiles[tileIndex];

        gameObject.transform.position = Game.instance.mapManager.map.GetPositionFromIndex(tileIndex);

        if(isInitialization == false)
            EndMovement();
	}
		
	public int getPointsMinions() {
		int total = 0;
		foreach(Minion minion in this._minions) {
			total += GameConfiguration.getPoints (minion.color);
		}
		return total;
	}

	public void SetSpeedBoostFx()
	{
		_currentTopFxGO = (GameObject) GameObject.Instantiate (_speedBoostFxPrefab, Vector3.zero , _speedBoostFxPrefab.transform.rotation);

		_currentTopFxGO.transform.SetParent (_topFxRoot.transform, false);

		SetColorToFx ();
	}

	public void SetDizzyStarsFx()
	{
		_currentTopFxGO = (GameObject) GameObject.Instantiate (_dizzyStarsPrefab, Vector3.zero , Quaternion.identity);

		_currentTopFxGO.transform.SetParent (_topFxRoot.transform, false);

		SetColorToFx ();
	}

	public void SetTimerFx()
	{
		_currentTopFxGO = (GameObject) GameObject.Instantiate (_timerFxPrefab, Vector3.zero, Quaternion.identity);

		_currentTopFxGO.transform.SetParent (_topFxRoot.transform, false);

		SetColorToFx ();
	}


	private void SetColorToFx()
	{
		Renderer[] renderers = _currentTopFxGO.GetComponentsInChildren<Renderer> ();

		for (int i = 0; i < renderers.Length; i++) {
			renderers [i].material.color = playerIndex == 0 ? Color.black : Color.white;
		}
	}

	public void RemoveTopFX()
	{
		Destroy (_currentTopFxGO);
	}
}