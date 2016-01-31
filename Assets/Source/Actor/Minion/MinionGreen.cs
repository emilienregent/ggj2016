using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Type;

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
		random = 2;
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
		MinionColor randomColor = (MinionColor) Random.Range (1, System.Enum.GetValues(typeof(MinionColor)).Length);
		List<int> obstacles = Game.instance.mapManager.map.GetArea (Game.instance.currentPlayer.tileIndex, (int)BonusConfiguration.REPLACE_OBSTACLES_AREA.x, (int)BonusConfiguration.REPLACE_OBSTACLES_AREA.y, TileType.OBSTACLE_FOREST_2);

		TileType minionTileType = Tile.getTileTypeForColor (randomColor);

		foreach(int obstacleIndex in obstacles) {
			Game.instance.mapManager.map.TransformTile (obstacleIndex, minionTileType);
			int numberOfMinions = Random.Range (BonusConfiguration.MIN_POP_MINIONS, BonusConfiguration.MAX_POP_MINIONS + 1);

			for (int i = 0; i < numberOfMinions; i++) {
				Minion minion = Game.instance.CreateMinion (randomColor);
				minion.tileIndex = obstacleIndex;
				minion.anchor = Game.instance.mapManager.map.tiles [obstacleIndex].gameObject.transform;
			}
		}

#if DEBUG
		Debug.Log ("All obstacles in the area have been transformed into minions");
#endif
	}

	// Move all obstacles to a new position
	private void RandomObstaclesPosition() {
		List<int> obstacles = Game.instance.mapManager.map.GetAllObstacles ();
		foreach(int obstacleIndex in obstacles) {
			int newTileIndex = Game.instance.mapManager.map.GetRandomAvailableIndex ();
			TileType obstacleType = (TileType) Game.instance.mapManager.map.tiles [obstacleIndex].type;
			Game.instance.mapManager.map.TransformTile (obstacleIndex, Type.TileType.DEFAULT);
			Game.instance.mapManager.map.TransformTile (newTileIndex, obstacleType);
		}
#if DEBUG
		Debug.Log("Move all obstacles to a new position");
#endif
	}

	// Create minions in the area
	private void GenerateMinions() {
		MinionColor randomColor = (MinionColor) Random.Range (1, System.Enum.GetValues(typeof(MinionColor)).Length);
		List<int> freeTiles = Game.instance.mapManager.map.GetArea (Game.instance.currentPlayer.tileIndex, (int)BonusConfiguration.GENERATE_MINIONS_AREA.x, (int)BonusConfiguration.GENERATE_MINIONS_AREA.y, TileType.DEFAULT);

		TileType minionTileType = Tile.getTileTypeForColor (randomColor);

		foreach (int freeTileIndex in freeTiles) {
			Game.instance.mapManager.map.TransformTile (freeTileIndex, minionTileType);
			int numberOfMinions = Random.Range (BonusConfiguration.MIN_POP_MINIONS, BonusConfiguration.MAX_POP_MINIONS + 1);

			for (int i = 0; i < numberOfMinions; i++) {
				Minion minion = Game.instance.CreateMinion (randomColor);
				minion.tileIndex = freeTileIndex;
				minion.anchor = Game.instance.mapManager.map.tiles [freeTileIndex].gameObject.transform;
			}
		}
#if DEBUG
		Debug.Log ("New minions created in the area");
#endif
	}

}