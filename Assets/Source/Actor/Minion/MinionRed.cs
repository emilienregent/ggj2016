using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Type;

public class MinionRed : Minion {

	// Use this for initialization
	private void Awake () {
		this.color = MinionColor.RED;
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
		Game.instance.GetNextPlayer ().KillAllMinions ();
#if DEBUG
		Debug.Log ("All minions of the other player have been killed");
#endif
	}

	// Invert the controls of the player, and play wierd sounds
	private void InvertControl() {
		Game.instance.GetNextPlayer ().ResetMalusPlayer ();
		Game.instance.GetNextPlayer ().speed *= -1;
		Game.instance.GetNextPlayer ().invertedControlLeft = BonusConfiguration.INVERTED_CONTROL_MALUS_LEFT;

		Game.instance.GetNextPlayer ().SetDizzyStarsFx ();
#if DEBUG
		Debug.Log ("The other player has his controls inverted");
#endif
	}

	// Disperse all the minions of the other player
	private void DisperseMinions() {
		List<int> freeTiles = Game.instance.mapManager.map.GetArea (Game.instance.GetNextPlayer ().tileIndex, (int)BonusConfiguration.DISPERSION_MINIONS_AREA.x, (int)BonusConfiguration.DISPERSION_MINIONS_AREA.y, TileType.DEFAULT);

		foreach(int tileIndex in freeTiles) {
			if (Game.instance.GetNextPlayer ().GetCountMinions (MinionColor.ANY) <= 0) {
				break;
			}

			MinionColor randomColor = Game.instance.GetNextPlayer ().GetMinion (MinionColor.ANY).color;

			List<Minion> minions = Game.instance.GetNextPlayer ().GetMinions (randomColor);
			foreach(Minion minion in minions) {
				Game.instance.GetNextPlayer ().RemoveMinion (minion.color);
				minion.anchor = Game.instance.mapManager.map.tiles [tileIndex].gameObject.transform;
			}
            Game.instance.GetNextPlayer().portrait.GetComponent<PlayerUI>().FillMinionSlots(Game.instance.GetNextPlayer().minions);
        }
#if DEBUG
		Debug.Log ("The minions of the other player have been dispersed");
#endif
	}

	protected override void PlayAppearFx ()
	{
		ParticleManager.instance.Play (ParticleManager.instance.appearSoldier, transform.position);
	}

	protected override void PlayDeathFx ()
	{
		ParticleManager.instance.Play (ParticleManager.instance.disappearSoldier, transform.position);
	}


}
