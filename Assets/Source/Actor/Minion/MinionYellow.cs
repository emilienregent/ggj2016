﻿using UnityEngine;
using System.Collections;
using Type;

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
		Game.instance.GetNextPlayer ().ResetMalusPlayer ();
		Game.instance.GetNextPlayer ().timeMalus = Game.instance.defaultDuration * BonusConfiguration.TIME_MALUS;
		Game.instance.GetNextPlayer ().timeMalusLeft = BonusConfiguration.TIME_MALUS_LEFT;
		Game.instance.GetNextPlayer ().SetTimerFx ();
#if DEBUG
		Debug.Log ("The other player will lost X second on his next turn");
#endif
	}

	// Move the player X tiles away
	private void IncreaseMovingSpeed() {
		Game.instance.currentPlayer.ResetMalusPlayer ();
		Game.instance.currentPlayer.speedBonus = BonusConfiguration.SPEED_BONUS;
		Game.instance.currentPlayer.SetSpeedBoostFx ();

#if DEBUG
		Debug.Log ("The player has moved X tiles away");
#endif
	}

	private void TeleportPlayer() {
		int randomIndex = Game.instance.mapManager.map.GetRandomAvailableIndex ();
		Game.instance.currentPlayer.Teleport (randomIndex);
#if DEBUG
		Debug.Log ("The player has been teleported to a random position");
#endif
	}

	protected override void PlayAppearFx ()
	{
		ParticleManager.instance.Play (ParticleManager.instance.appearFlash, transform.position);
	}

	protected override void PlayDeathFx ()
	{
		ParticleManager.instance.Play (ParticleManager.instance.disappearFlash, transform.position);
	}

}
