using System;
using UnityEngine;

namespace Type {

	public class BonusConfiguration {

		public static float 	TIME_MALUS 				= 0.5f;
		public static int		TIME_MALUS_LEFT			= 2;

		public static Vector2	REPLACE_OBSTACLES_AREA	= new Vector2(3, 3);

		public static int		SPEED_BONUS 			= 3;

		public static Vector2 	GENERATE_MINIONS_AREA 	= new Vector2(3, 3);

		public static Vector2	DISPERSION_MINIONS_AREA = new Vector2(3, 3);

		public static int		MIN_POP_MINIONS			= 0;
		public static int		MAX_POP_MINIONS			= 2;

		public BonusConfiguration() {
			
		}
	}
}