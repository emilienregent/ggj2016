using System;
using UnityEngine;

namespace Type
{
    public class Tile
    {
        private int  _type = 0;
        public  int  type  { get { return _type; } }

        public Tile(int type)
        {
            _type = type;
        }
    }
}