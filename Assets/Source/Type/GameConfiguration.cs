using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameConfiguration {

	public 	static 	int MAX_SCORE = 7;

	private	static	int RED_POINT = 1;
	private	static	int GREEN_POINT = 1;
	private	static	int BLUE_POINT = 1;
	private	static	int YELLOW_POINT = 1;

    public  static  int NUMBER_OF_MINION_TRESHOLD = 50;
    public  static  int TURN_BEFORE_RESPAWN = 3;

	public static	float MINION_RANGE = 4f;

	private static	float MINION_SPEED_IDLE_BLUE = 5f;
	private static	float MINION_SPEED_IDLE_GREEN = 5f;
	private static	float MINION_SPEED_IDLE_RED = 5f;
	private static	float MINION_SPEED_IDLE_YELLOW = 5f;
	private static 	float MINION_SPEED_MULTIPLICATOR = 2f;

	public static int getPoints(MinionColor color) {
		switch(color) {
			case MinionColor.BLUE:
				return BLUE_POINT;
			case MinionColor.GREEN:
				return GREEN_POINT;
			case MinionColor.RED:
				return RED_POINT;
			case MinionColor.YELLOW:
				return YELLOW_POINT;
			default: 
				return 0;
		}
	}

	public static float getMinionSpeed(MinionColor color, bool isIdle) {
		float speed = 0f;

		switch(color) {
			case MinionColor.BLUE:
				speed = MINION_SPEED_IDLE_BLUE;
				break;
			case MinionColor.GREEN:
				speed = MINION_SPEED_IDLE_GREEN;
				break;
			case MinionColor.RED:
				speed = MINION_SPEED_IDLE_RED;
				break;
			case MinionColor.YELLOW:
				speed = MINION_SPEED_IDLE_YELLOW;
				break;
			default: 
				return 0f;
		}

		if(isIdle == true) {
			return speed;
		} else {
			return speed * MINION_SPEED_MULTIPLICATOR;
		}
	}

	public GameConfiguration() {
		
	}
}
