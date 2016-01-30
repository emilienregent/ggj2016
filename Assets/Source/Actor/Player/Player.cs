using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
//	[SerializeField]
//	private			Taille			_position 		= null;
	private			bool			_canAction		= 	false;


	// Use this for initialization
	private void Start ()
	{
		Debug.Log ("New player (" + this.name + ") created with " + this._countActions + " action(s) remaining.");
//		ChangeColor(Color.blue);
	}

	// Add a minion to the player
	public void AddMinion(Minion minion) {
			Debug.Log ("One minion with color " + minion.GetColor () + " added to the player");
			minion.name = "Minion " + minion.GetColor() + " " + (this._minions.Count+1);
			this._minions.Add (minion);
	}

	// Remove a minion from the player
	public void RemoveMinion(MinionColor color) {
		if (this._minions.Count > 0) {
			Minion minionToRemove = null;
			foreach (Minion minion in this._minions) {
				if (minion.GetColor () == color) {
					minionToRemove = minion;
					break;
				}
			}
			if (minionToRemove != null) {
				this._minions.Remove (minionToRemove);
				Debug.Log ("One minion with color " + minionToRemove.GetColor () + " has been removed");
			} else {
				Debug.Log ("ERROR NO MINION WITH COLOR " + color + " FOUND !");
			}
		} else {
			Debug.Log ("The player hasn't any minions.");
		}
	}

	public bool CanAddMinion() {
		return this._minions.Count < _MAX_MINIONS;
	}

	// Check if the player can do an action
	public bool CanAction() {
		return this._canAction;
	}

	// Set if the player can do an action or not
	public void SetAction(bool canAction) {
		this._canAction = canAction;
	}

	// Sacrifice a minion to get a bonus/special action
	public void SacrificeMinion(Minion minion) {
		if(this.CanAction() == true) {
			minion.Sacrifice ();
			this.SetAction (false);
		}
	}

	// Return the total of minions of the player
	public int getCountMinions() {
		return this._minions.Count;
	}

//	public void setTaille(Taille position) {
//		this._position = _position;
//	}
//
//	public Taille getTaille() {
//		return this._position;
//	}

	// Set the score of the player
	public void SetScore(int score) {
		this._score = score;
	}

	// Get the score of the player
	public int GetScore() {
		return this._score;
	}

	public void Release ()
	{
	}

	public void MoveLeft()
	{
		Debug.Log ("Move Left !");
//		Move(-1f, 0f);
	}

	public void MoveRight()
	{
		Debug.Log ("Move Right !");
//		Move(1f, 0f);
	}

	public void MoveUp()
	{
		Debug.Log ("Move Upt !");
//		Move(0f, 1f);
	}

	public void MoveDown()
	{
		Debug.Log ("Move Down !");
//		Move(0f, -1f);
	}

	private void Move (float x, float y)
	{
		if (this.CanAction () == true) {
			gameObject.transform.position += new Vector3 (x * _speed, y * _speed);

			AudioManager.instance.plop.Play ();
			this.SetAction (false);
		}
	}

	private void ChangeColor(Color color)
	{
		GetComponent<SpriteRenderer>().color = color;
	}
}