using System;
using UnityEngine;

namespace Type
{
    public enum TileType
    {
        DEFAULT,
        OBSTACLE,
        MINION
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
    }
}