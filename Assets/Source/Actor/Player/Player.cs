using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Type;

public class Player : MonoBehaviour
{
	// Constantes
	[SerializeField]
	private const 	int 			_MAX_MINIONS 	= 	6;

	// Privates attributes
	[SerializeField]
	private 		float 			_speed 			= 	0.1f;
	[SerializeField]
	private 		int 			_countActions 	= 	1;
	[SerializeField]
	private 		List<Minion> 	_minions 		= 	new List<Minion>();
	[SerializeField]
	private 		int 			_score 			= 	0;
	[SerializeField]
	private			Tile			_position 		= 	null;
	private			bool			_canAction		= 	false;

	public			Tile			position		{ get { return _position; } set { _position = value; } }
	public			int				score			{ get { return _score; } set { _score = value; } }
	public			bool			canAction		{ get { return _canAction; } }



	// Use this for initialization
	private void Start ()
	{
		#if DEBUG
			Debug.Log ("New player (" + this.name + ") created with " + this._countActions + " action(s) remaining.");
		#endif
//		ChangeColor(Color.blue);
	}

	// Add a minion to the player
	public void AddMinion(Minion minion) {
		#if DEBUG
				Debug.Log ("One minion with color " + minion.color + " added to the player");
		#endif
		minion.name = "Minion " + minion.color + " " + (this._minions.Count+1);
		minion.transform.SetParent (this.transform.parent.transform);
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
        if (canAction == false)
        {
            Game.instance.SetNextPlayer();
        }
    }

	// Sacrifice a minion to get a bonus/special action
	public void SacrificeMinion(MinionColor color) {
		if(this.canAction == true) {
            // Todo : A décommenter
			// minion.Sacrifice ();
            switch (color)
            {
                case MinionColor.GREEN:
                    UseMinionGreen();
                    break;

                case MinionColor.RED:
                    UseMinionRed();
                    break;

                case MinionColor.YELLOW:
                    UseMinionYellow();
                    break;

                case MinionColor.BLUE:
                    UseMinionBlue();
                    break;

            }
			this.SetAction(false);
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
		if (this.GetCountMinions () > 0) {
			int randomIndex = Random.Range (0, this.GetCountMinions ());
			return this._minions [randomIndex];
		} else {
			#if DEBUG
			Debug.Log ("ERROR PLAYER " + this.name + " DOESN'T HAVE MINIONS");
			#endif
			return null;
		}
	}

	// Return the total of minions of the player
	public int GetCountMinions() {
		return this._minions.Count;
	}

	// Return the total of blue minions
	public int GetCountMinionsBlue() {
		int totalMinionBlue = 0;
		foreach(Minion minion in this._minions) {
			if(minion.color == MinionColor.BLUE) {
				totalMinionBlue++;
			}
		}

		return totalMinionBlue;
	}

	// Return the total of green minions
	public int GetCountMinionsGreen() {
		int totalMinionGreen = 0;
		foreach(Minion minion in this._minions) {
			if(minion.color == MinionColor.GREEN) {
				totalMinionGreen++;
			}
		}

		return totalMinionGreen;
	}

	// Return the total of red minions
	public int GetCountMinionsRed() {
		int totalMinionRed = 0;
		foreach(Minion minion in this._minions) {
			if(minion.color == MinionColor.RED) {
				totalMinionRed++;
			}
		}

		return totalMinionRed;
	}

	// Return the total of yellow minions
	public int GetCountMinionsYellow() {
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

	public void MoveLeft()
	{
		#if DEBUG
			Debug.Log ("Move Left !");
		#endif
//		Move(-1f, 0f);
	}

	public void MoveRight()
	{
		#if DEBUG
			Debug.Log ("Move Right !");
		#endif
//		Move(1f, 0f);
	}

	public void MoveUp()
	{
		#if DEBUG
			Debug.Log ("Move Upt !");
		#endif
//		Move(0f, 1f);
	}

	public void MoveDown()
	{
		#if DEBUG
			Debug.Log ("Move Down !");
		#endif
//		Move(0f, -1f);
	}

	public void UseMinionGreen()
	{
		ChangeColor(Color.green);
	}

	public void UseMinionBlue()
	{
		ChangeColor(Color.blue);
	}

	public void UseMinionYellow()
	{
		ChangeColor(Color.yellow);
	}

	public void UseMinionRed()
	{
		ChangeColor(Color.red);
	}

	private void Move(float x, float y)
	{
		if (this.canAction == true) {
			gameObject.transform.position += new Vector3 (x * _speed, y * _speed);

			AudioManager.instance.plop.Play ();
			this.SetAction(false);
		}
	}

	private void ChangeColor(Color color)
	{
		GetComponent<SpriteRenderer>().color = color;
	}
}