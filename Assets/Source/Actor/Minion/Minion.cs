﻿using UnityEngine;
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
	protected	int				_maxPowers		=	0;
	[SerializeField]
	protected 	MinionColor 	_color;
	[SerializeField]
	private		int			    _tileIndex 		= 	0;
	protected 	bool			_canSacrifice 	= 	true;

	public		MinionColor		color			{ get { return _color; } set { _color = value; } }
	public		bool			canSacrifice	{ get { return _canSacrifice; } set { _canSacrifice = value; } }
	public		int			    tileIndex		{ get { return _tileIndex; } set { 
			_tileIndex = value; 
			gameObject.transform.position = Game.instance.mapManager.map.GetPositionFromIndex(value);
		} }
	
	// Trigger one bonus of the minion
	public void Sacrifice() {
		if(this.canSacrifice == true) {
			#if DEBUG
				Debug.Log ("Minion's power activated !");
			#endif
			this.ExecuteRandomPower ();
		}

	}

	// Kill the minion
	public void Kill() {
		Destroy (this);
	}

	// Execute one random power of the minion
	protected abstract void ExecuteRandomPower ();

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
