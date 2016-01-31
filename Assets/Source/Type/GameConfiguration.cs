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

	public GameConfiguration() {
		
	}
}
