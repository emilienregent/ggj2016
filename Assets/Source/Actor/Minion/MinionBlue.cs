using UnityEngine;
using System.Collections;

public class MinionBlue : Minion {
	private	const	int	_MAX_POWERS	=	4;

	// Use this for initialization
	private void Awake () {
		this.color = MinionColor.BLUE;
	}
	
	// Update is called once per frame
	private void Update () {
	
	}

	// Execute one random power of the minion
//	protected void ExecuteRandomPower() {
//		int random = Random.Range (0, _MAX_POWERS);
//
//		switch(random) {
//			case 0:
//				return this.MoveAltar ();
//			case 1:
//				return this.WalkThroughObstacle ();
//			case 2:
//				return this.RevealMinions ();
//			case 3:
//				return this.InfectMinions ();
//		}
//	}
//
//	// Move the Altar to an other location
//	private void MoveAltar() {
//		Debug.Log ("The Altar has been moved !");
//	}
//
//	// Move through an obstacle
//	private void WalkThroughObstacle() {
//		Debug.Log ("Move through an obstacle");
//	}
//
//	// Reveals all the minions of the map
//	private void RevealMinions() {
//		Debug.Log ("All minions have been revealed.");
//	}
//
//	// Infect all the minions of the other player, they can't be sacrificed
//	private void InfectMinions() {
//		Debug.Log ("All the minions of the other player have been infected.");
//	}
}