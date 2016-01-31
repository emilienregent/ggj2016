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

        // Retourne toutes les tiles TYPE dans la zone X*Y autour de indexTile
        // permet de récupérer tous les obstacles dans la zone
        // permet de récupérer toutes les positions valides dans la zone
        public List<int> GetArea(int indexTile, int columns, int lines, TileType type) 
        {
            if (lines % 2 == 0)
            {
                lines++;
            }

            if (columns % 2 == 0)
            {
                columns++;
            }  

            List<int>   indexes = new List<int>();
            int         left    = Mathf.Max(0, indexTile - Mathf.FloorToInt(columns * 0.5f));


            // Get left border
            if (IsOnSameLine(indexTile, left) == false)
            {
                left = indexTile - indexTile % _columns;
                columns--;
            }

            // Goes up to get top-left corner
            int start = left - Mathf.FloorToInt(lines * 0.5f) * _columns;

            while(start < 0)
            {
                start = left - Mathf.FloorToInt(--lines * 0.5f) * _columns;
            }

            int index = start;

            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    int nextIndex = start + j + i * _columns;

                    if (nextIndex >= _tiles.Count)
                    {
                        return indexes;
                    }
                    else if (IsOnSameLine(index, nextIndex) == true || j == 0)
                    {
                        index = nextIndex;

                        if (_tiles[index].type == (int)type || ((int)type >= (int)TileType.OBSTACLE_FOREST_1 && _tiles[index].type >= (int)TileType.OBSTACLE_FOREST_1))
                        {
                            indexes.Add(index);
                        }
                    }
                }
            }
			indexes.Remove (indexTile);
            return indexes;
        }

        public List<int> GetAllObstacles() 
        {
            List<int> indexes = new List<int>();

            for (int i = 0; i < _tiles.Count; i++)
            {
                Tile tile = _tiles[i];

                if (tile.type >= (int)TileType.OBSTACLE_FOREST_1)
                {
                    indexes.Add(i);
                }
            }

            return indexes;
        }
        
        public void TransformTile(int indexTile, TileType type) 
        {
            if (indexTile < _tiles.Count)
            {
                _tiles[indexTile].SetType(type);
            }
        }
        
        public void MoveAltar(int indexTile) 
        {
            int currentIndexOfAltar = 0;

            for (int i = 0; i < _tiles.Count; i++)
            {
                Tile tile = _tiles[i];

                if (tile.type == (int)TileType.ALTAR)
                {
                    currentIndexOfAltar = i;
                    break;
                }
            }

            if (indexTile < _tiles.Count)
            {
                TransformTile(indexTile, TileType.ALTAR);
                TransformTile(currentIndexOfAltar, TileType.DEFAULT);
            }
        }

        public bool IsOnSameLine(int currentTileIndex, int newTileIndex)
        {
            // Horizontal move ?
            int delta = newTileIndex - currentTileIndex;

            // Same line or not
            if(delta > 0)
            {
                return (newTileIndex % _columns > currentTileIndex % _columns);
            }
            if(delta < 0)
            {
                return (newTileIndex % _columns < currentTileIndex % _columns);
            }

            return false;
        }
    }
}