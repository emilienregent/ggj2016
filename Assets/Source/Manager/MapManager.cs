using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Type;
using Helper.Parser;

namespace Manager
{
    public class MapManager : IManager
    {
        [SerializeField]
        private GameObject[] _gameObjects = new GameObject[0];

        private List<Map>   _maps   = new List<Map>();
        private Map         _map    = null;
        private GameObject  _root   = null;
        public  Map         map     { get { return _map; } }

        // Use this for initialization
        override public void Initialize()
        {
            type = ManagerType.MAP;

            _root   = new GameObject("Map");
            _maps   = MapParser.Parse();
            _map    = _maps[Game.instance.mapIndex];

    #if DEBUG
            Debug.Log("Create a map with " + _map.tiles.Count + " tiles on " + _map.lines + " lines and " + _map.columns + " columns");
    #endif

            for (int i = 0; i < _map.tiles.Count; i++)
            {
                Tile        tile    = _map.tiles[i];
                GameObject  go      = tile.type < _gameObjects.Length ? _gameObjects[tile.type] : _gameObjects[0];
                GameObject  tileGO  = GameObject.Instantiate(go);
                tile.gameObject = tileGO;

                tileGO.name = "Tile " + i;
                tileGO.transform.position = _map.GetPositionFromIndex(i);
                tileGO.transform.SetParent(_root.transform);
            }
        }
    }
}