using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Type;
using Helper.Parser;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _gameObjects = new GameObject[0];

    private List<Map>   _maps   = new List<Map>();
    private Map         _map    = null;
    private GameObject  _root   = null;

    private void Awake()
    {
        Debug.Log("Awake Map Manager");
    }

    // Use this for initialization
    private void Start()
    {
        Debug.Log("Start Map Manager");

        _root   = new GameObject("Map");
        _maps   = MapParser.Parse();
        _map    = _maps[Game.instance.mapIndex];

        Debug.Log("Create a map with " + _map.tiles.Count + " tiles on " + _map.lines + " lines and " + _map.columns + " columns");

        for (int i = 0; i < _map.tiles.Count; i++)
        {
            Tile        tile    = _map.tiles[i];
            GameObject  tileGO  = Instantiate(_gameObjects[tile.type]);

            tileGO.name = "Tile " + i;
            tileGO.transform.position = new Vector3(- (i % _map.columns) * tile.size, 0f, (i / _map.columns) * tile.size);
            tileGO.transform.SetParent(_root.transform);
        }
    }
}