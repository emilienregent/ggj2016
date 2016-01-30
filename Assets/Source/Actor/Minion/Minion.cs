using UnityEngine;
using System.Collections;

public enum MinionColor
{
	GREEN,
	RED, 
	BLUE,
	YELLOW
}


public abstract class Minion : MonoBehaviour {

	[SerializeField]
	protected 	MinionColor 	_color;

	// Trigger one bonus of the minion, and kill the minion
	public void Sacrifice() {
		Debug.Log ("Minion's power activated !");
		// TODO : destroy minion object
	}

	// Use this for initialization
	private void Start () {
		Debug.Log ("New Minion with color " + this._color);
	}
	
	// Update is called once per frame
	private void Update () {
	
	}

	// Return the color of the minion
	public MinionColor GetColor() {
		return this._color;
	}

	// Set the color of the minion
	public void SetColor(MinionColor color) {
		this._color = color;
	}
}
