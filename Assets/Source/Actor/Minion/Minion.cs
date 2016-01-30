using UnityEngine;
using System.Collections;
using Type;

public enum MinionColor
{
	ANY,
	GREEN,
	RED, 
	BLUE,
	YELLOW
}


public abstract class Minion : MonoBehaviour {

	[SerializeField]
	protected 	MinionColor 	_color;
	[SerializeField]
	protected	Tile 			_position	=	null;
	public		MinionColor		color		{ get { return _color; } set { _color = value; } }

	public		Tile			position	{ get { return _position; } set { _position = value; } }

	// Trigger one bonus of the minion, and kill the minion
	public void Sacrifice() {
		#if DEBUG
			Debug.Log ("Minion's power activated !");
		#endif
//		this.ExecuteRandomPower ();
		// TODO : destroy minion object
	}

	// Execute one random power of the minion
//	protected abstract void ExecuteRandomPower ();

	// Use this for initialization
	private void Start () {
		#if DEBUG
			Debug.Log ("New Minion with color " + this._color);
		#endif
	}
	
	// Update is called once per frame
	private void Update () {
	
	}

}
