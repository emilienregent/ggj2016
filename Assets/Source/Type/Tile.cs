using System;
using UnityEngine;

namespace Type
{
    public enum TileType
    {
        DEFAULT,
        START_P1,
        START_P2,
        ALTAR,
        MINION_GREEN,
        MINION_RED, 
        MINION_BLUE,
        MINION_YELLOW,
        OBSTACLE_1,
        OBSTACLE_2
    }

    public class Tile
    {
        private int _size   = 10;
        private int _type   = 0;

        public  int size    { get { return _size; } set { _size = value; } }
        public  int type    { get { return _type; } set { _type = value; } }

        public Tile(int type)
        {
            _type = type;
        }

        public void ApplyOnPlayer(Player player)
        {
            switch ((TileType) type)
            {
                case TileType.MINION_RED : CreateMinion(player, MinionColor.RED);
                    break;
                case TileType.MINION_BLUE : CreateMinion(player, MinionColor.BLUE);
                    break;
                case TileType.MINION_GREEN : CreateMinion(player, MinionColor.GREEN);
                    break;
                case TileType.MINION_YELLOW : CreateMinion(player, MinionColor.YELLOW);
                    break;
            }
        }

        private void CreateMinion(Player player, MinionColor color)
        {
            if (player.CanAddMinion() == true)
            {
                player.AddMinion(Game.instance.CreateMinion(color));
            }
        }
    }
}