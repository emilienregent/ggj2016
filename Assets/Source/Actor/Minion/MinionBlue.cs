using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinionBlue : Minion {
	

	// Use this for initialization
	private void Awake () {
		this.color = MinionColor.BLUE;
		this._maxPowers = 3;
	}
	
	// Update is called once per frame
    override public void Update () {
        base.Update();
	}

	// Execute one random power of the minion
	override protected void ExecuteRandomPower() {
		int random = Random.Range (0, this._maxPowers);

		switch(random) {
			case 0:
				this.MoveAltar ();
				break;
			case 1:
				this.SwitchMinions ();
				break;
			case 2:
				this.SwitchPlayersPositions ();
				break;
		}
	}

	// Move the Altar to an other location
	private void MoveAltar() {
		int newIndexPosition = Game.instance.mapManager.map.GetRandomAvailableIndex ();
		Game.instance.mapManager.map.MoveAltar (newIndexPosition);
#if DEBUG
		Debug.Log ("The Altar has been moved !");
#endif
	}

	// Switch the minions of the two players.
	private void SwitchMinions() {
		List<Minion> currentPlayerMinions = Game.instance.currentPlayer.minions;
		List<Minion> nextPlayerMinions = Game.instance.GetNextPlayer ().minions;

		Game.instance.currentPlayer.minions = nextPlayerMinions;
		Game.instance.GetNextPlayer ().minions = currentPlayerMinions;

		foreach(Minion minion in Game.instance.currentPlayer.minions) {
			minion.tileIndex = Game.instance.currentPlayer.tileIndex;
			minion.anchor = Game.instance.currentPlayer.transform;
		}

		foreach(Minion minion in Game.instance.GetNextPlayer ().minions) {
			minion.tileIndex = Game.instance.GetNextPlayer ().tileIndex;
			minion.anchor = Game.instance.GetNextPlayer ().transform;
		}

#if DEBUG
		Debug.Log ("Minions of the 2 players have been switched !");
#endif
	}

	// Switch the position of the two players
	private void SwitchPlayersPositions() {
		int currentPlayerIndexTile = Game.instance.currentPlayer.tileIndex;
		int nextPlayerIndexTile = Game.instance.GetNextPlayer ().tileIndex;
		Game.instance.currentPlayer.tileIndex = 0;
		Game.instance.GetNextPlayer ().tileIndex = currentPlayerIndexTile;
		Game.instance.currentPlayer.tileIndex = nextPlayerIndexTile;

		List<Minion> currentPlayerMinions = Game.instance.currentPlayer.minions;
		List<Minion> nextPlayerMinions = Game.instance.GetNextPlayer ().minions;

		foreach(Minion minion in currentPlayerMinions) {
			minion.tileIndex = Game.instance.currentPlayer.tileIndex;
			minion.anchor = Game.instance.currentPlayer.transform;
		}

		foreach(Minion minion in nextPlayerMinions) {
			minion.tileIndex = Game.instance.GetNextPlayer ().tileIndex;
			minion.anchor = Game.instance.GetNextPlayer ().transform;
		}

#if DEBUG
		Debug.Log ("Position of the 2 players has been switched !");
#endif
	}
}