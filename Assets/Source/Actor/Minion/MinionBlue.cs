using UnityEngine;
using System.Collections;

public class MinionBlue : Minion {
	

	// Use this for initialization
	private void Awake () {
		this.color = MinionColor.BLUE;
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
		// TODO : Game.instance.getAltarPosition(); Game.instance.getRandomPosition(); Game.instance.Altar.setPosition()
#if DEBUG
		Debug.Log ("The Altar has been moved !");
#endif
	}

	// Switch the minions of the two players.
	private void SwitchMinions() {
		// TODO : getMinions P2, setMinionsP2(minionsP1), setMinionsP1(minionsP2)
		// TODO : setPositions of minions
#if DEBUG
		Debug.Log ("Minions of the 2 players have been switched !");
#endif
	}

	// Switch the position of the two players
	private void SwitchPlayersPositions() {
		// TODO : moveP1ToP2, moveP2ToP1; moveMinionsP1; moveMinionsP2;
#if DEBUG
		Debug.Log ("Position of the 2 players has been switched !");
#endif
	}

	// Move through an obstacle
	private void WalkThroughObstacle() {
		// TODO : set player flag to true
#if DEBUG
		Debug.Log ("Move through an obstacle");
#endif
	}

	// Reveals all the minions of the map
	private void RevealMinions() {
		// TODO : flag on minions ? (wild/owned) OR only close minions ?
#if DEBUG
		Debug.Log ("All minions have been revealed.");
#endif
	}

	// Infect all the minions of the other player, they can't be sacrificed
	private void InfectMinions() {
		// TODO : set minions canBeSacrifice to false
#if DEBUG
		Debug.Log ("All the minions of the other player have been infected.");
#endif
	}
}