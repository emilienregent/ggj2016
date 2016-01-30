using UnityEngine;
using System.Collections;

public class MinionGreen : Minion {

	// Use this for initialization
	private void Awake () {
		this.color = MinionColor.GREEN;
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
				this.TransformObstaclesIntoMinions ();
				break;
			case 1:
				this.RandomObstaclesPosition ();
				break;
			case 2:
				this.GenerateMinions ();
				break;
		}
	}

	// Transform all obstacles in an area into minions
	private void TransformObstaclesIntoMinions() {
		// TODO : range area, select all obtascle in range, transform obstacles into normal tile, pop minions;
#if DEBUG
		Debug.Log ("All obstacles in the area have been transformed into minions");
#endif
	}

	// Move all obstacles to a new position
	private void RandomObstaclesPosition() {
		// TODO : select all obtascles; foreach obstacles set randomposition;
#if DEBUG
		Debug.Log("Move all obstacles to a new position");
#endif
	}

	// Create minions in the area
	private void GenerateMinions() {
		// TODO : getRangeArea foreach tile in the area : generateRandomMinions
#if DEBUG
		Debug.Log ("New minions created in the area");
#endif
	}

}
