using System.Collections.Generic;
using UnityEngine;

namespace Type
{
    public class Map
    {
        private int         _lines      = 0;
        private int         _columns    = 0;
        private List<Tile>  _tiles      = new List<Tile>();

        public  int         lines       { get { return _lines; } set { _lines = value; } }
        public  int         columns     { get { return _columns; } set { _columns = value; } }
        public  List<Tile>  tiles       { get { return _tiles; } set { _tiles = value; } }

        public Map(int lines, int columns)
        {
            _lines      = lines;
            _columns    = columns;
        }

        public int GetIndexFromPosition(int x, int y)
        {
            return x + y * _columns;
        }

        public Vector3 GetPositionFromIndex(int index)
        {
            if (index < _tiles.Count)
            {
                int size = _tiles[index].size;

                return new Vector3(- (index % _columns) * size, 0f, (index / _columns) * size);
            }

            return new Vector3();
        }

        public int GetStartIndex(TileType type)
        {
            for (int i = 0; i < _tiles.Count; i++)
            {
                if (_tiles[i].type == (int)type)
                {
                    return i;
                }
            }

            return 0;
        }

        public int GetRandomAvailableIndex()
        {
            List<int> indexes = new List<int>();

            for (int i = 0; i < _tiles.Count; i++)
            {
                Tile tile = _tiles[i];

                if (tile.type == (int)TileType.DEFAULT)
                {
                    bool hasPlayer = false;

                    for (int j = 0; j < Game.instance.players.Count; j++)
                    {
                        if (Game.instance.players[j].tileIndex == i)
                        {
                            hasPlayer = true;
                        }
                    }

                    if (hasPlayer == false)
                    {
                        indexes.Add(i);
                    }
                }
            }

            return indexes[Random.Range(0, indexes.Count)];
        }
    }

//	public List<int> GetTileInArea(int indexTile, int x, int y, TileType type) {
//		// Retourne toutes les tiles TYPE dans la zone X*Y autour de indexTile
//		// permet de récupérer tous les obstacles dans la zone
//		// permet de récupérer toutes les positions valides dans la zone
//	}

//	public List<int> GetAllObstacles() {
//		// Retourne toutes les positions des obstacles
//	}

//	public void TransformTile(int indexTile, TileType type) {
//		// Transforme la tile à la position indexTile en tile de type type
//	}

//	public void MoveAltar(int indexTile) {
//		// Déplace l'autel à la position indexTile
//	}

}