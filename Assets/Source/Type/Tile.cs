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
        private GameObject  _gameObject = null;
        private int         _size       = 10;
        private int         _type       = 0;
        private int         _index      = -1;

        public  GameObject  gameObject  { get { return _gameObject; } set { _gameObject = value; } }
        public  int         size        { get { return _size; } set { _size = value; } }
        public  int         type        { get { return _type; } set { _type = value; } }
        public  int         index       { get { return _index; } }

        public Tile(int type, int index = -1)
        {
            _type   = type;
            _index  = index;
        }

        public void ApplyOnPlayer(Player player)
        {
            switch ((TileType) type)
            {
                case TileType.MINION_RED: 
                case TileType.MINION_BLUE:
                case TileType.MINION_GREEN:
                case TileType.MINION_YELLOW:
                    if (Game.instance.tileToMinions.ContainsKey(_index) == true)
                    {
                        Game.instance.tileToMinions[_index].AnchorToPlayer(player);
                    }
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

        public void SetType(TileType type)
        {
            _type = (int)type;

            Game.instance.mapManager.RefreshTile(this);
        }
    }
}