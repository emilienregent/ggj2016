using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Type;
using Helper.Parser;

public class MapManager : MonoBehaviour
{
    public static int LINES      = 10;
    public static int COLUMNS    = 6;

    [SerializeField]
    private GameObject[] _gameObjects = new GameObject[0];

    private List<Map>   _maps   = new List<Map>();
    private Map         _map    = null;
    private GameObject  _root   = null;

    private void Awake()
    {
        
    }

    // Use this for initialization
    private void Start()
    {
        _root   = new GameObject("Map");
        _maps   = MapParser.Parse();
        _map    = _maps[Game.instance.mapIndex];

        for (int i = 0; i < _map.tiles.Count; i++)
        {
            Tile        tile    = _map.tiles[i];
            GameObject  tileGO  = Instantiate(_gameObjects[tile.type]);

            tileGO.name = "Tile " + i;
            tileGO.transform.position = new Vector3(- (i % COLUMNS) * tile.size, 0f, (i / COLUMNS) * tile.size);
            tileGO.transform.SetParent(_root.transform);
        }
    }
}