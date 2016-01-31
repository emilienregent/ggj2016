using System;
using UnityEngine;

namespace Type {

	public class BonusConfiguration {

		// Temps diminué
		public static float 	TIME_MALUS 				= 0.5f;
		public static int		TIME_MALUS_LEFT			= 3;

		// Controles inversés
		public static int		INVERTED_CONTROL_MALUS_LEFT = 2;

		// Remplacer les obstacles par des minions
		public static Vector2	REPLACE_OBSTACLES_AREA	= new Vector2(3, 3);

		// Se déplacer de X cases au prochain tour
		public static int		SPEED_BONUS 			= 3;

		// Générer des minions sur les cellules vides
		public static Vector2 	GENERATE_MINIONS_AREA 	= new Vector2(3, 3);

		// Disperser les minions de l'adversaire
		public static Vector2	DISPERSION_MINIONS_AREA = new Vector2(3, 3);

		// Quantité min/max de minions à générer sur une zone
		public static int		MIN_POP_MINIONS			= 0;
		public static int		MAX_POP_MINIONS			= 2;

		public BonusConfiguration() {
			
		}
	}
}