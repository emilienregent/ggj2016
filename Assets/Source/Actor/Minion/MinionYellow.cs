using UnityEngine;
using System.Collections;

public class MinionYellow : Minion {

	// Use this for initialization
	private void Awake () {
		this.color = MinionColor.YELLOW;
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
				this.ReduceTime ();
				break;
			case 1:
				this.IncreaseMovingSpeed ();
				break;
			case 2:
				this.TeleportPlayer ();
				break;
		}
	}

	// Reduce the timer of the other player
	private void ReduceTime() {
#if DEBUG
		Debug.Log ("The other player will lost X second on his next turn");
#endif
		// TODO : getOtherPlayer, player.timeMalus = X;
	}

	// Move the player X tiles away
	private void IncreaseMovingSpeed() {
#if DEBUG
		Debug.Log ("The player has moved X tiles away");
#endif
		// TODO : select a random direction, move X
	}

	private void TeleportPlayer() {
#if DEBUG
		Debug.Log ("The player has been teleported to a random position");
#endif
		// TODO : select one valid position, move player to this position
	}
}
