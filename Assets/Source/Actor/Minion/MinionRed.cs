﻿using UnityEngine;
using System.Collections;

public class MinionRed : Minion {

	// Use this for initialization
	private void Awake () {
		this.color = MinionColor.RED;
		this._maxPowers = 3;
	}
	
	// Update is called once per frame
	private void Update () {
	
	}

	// Execute one random power of the minion
	override protected void ExecuteRandomPower() {
		int random = Random.Range (0, this._maxPowers);

		switch(random) {
			case 0:
				this.KillAllMinions ();
				break;
			case 1:
				this.InvertControl ();
				break;
			case 2:
				this.DisperseMinions ();
				break;
		}
	}

	// Kill all the minions of the other player
	private void KillAllMinions() {
		// TODO : foreach P2 minons ; p2.remove; minion.kill();
#if DEBUG
		Debug.Log ("All minions of the other player have been killed");
#endif
	}

	// Invert the controls of the player, and play wierd sounds
	private void InvertControl() {
#if DEBUG
		Debug.Log ("The other player has his controls inverted");
#endif
		// TODO : P2.controlManager.invert;
	}

	// Disperse all the minions of the other player
	private void DisperseMinions() {
#if DEBUG
		Debug.Log ("The minions of the other player have been dispersed");
#endif
		// TODO : getValidPositionsAroundP2, foreach positions put all minions of color X		
	}
}
