using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Type;
using SimpleJSON;
using System.IO;

public class MapManager : MonoBehaviour
{

    private static string MAP_FOLDER    = "Assets/Resources/";
    private static string MAP_FILENAME  = "map";
    private static string MAP_EXTENSION = ".json";

    private static int LINES      = 8;
    private static int COLUMNS    = 6;

    [SerializeField]
    private GameObject[] _gameObjects = new GameObject[0];

    private List<Tile> _tiles   = new List<Tile>();
    private GameObject _root    = null;

    private void Awake()
    {
        _root   = Instantiate(new GameObject("Map"));
        _tiles  = Parse();

        for (int i = 0; i < _tiles.Count; i++)
        {
            GameObject tile = Instantiate(_gameObjects[_tiles[i].type]);

            tile.name = "Tile " + i;
            tile.transform.position = new Vector3(- (i % COLUMNS) * tile.transform.localScale.x, 0f, (i / COLUMNS) * tile.transform.localScale.z);
            tile.transform.SetParent(_root.transform);
        }
    }

    // Use this for initialization
    private void Start()
    {
	    
    }

    public static List<Tile> Parse()
    {
        TextAsset   configuration   = Resources.Load(MAP_FILENAME) as TextAsset;
        List<Tile>  tiles           = new List<Tile>();

        if (configuration != null)
        {
            JSONNode json = JSON.Parse(configuration.text);

            for (int i = 0; i < LINES; i++)
            {
                for (int j = 0; j < COLUMNS; j++)
                {
                    int index   = i * LINES * COLUMNS + j;
                    int type    = json["maps"][index].AsInt;
                    
                    tiles.Add(new Tile(type));
                }
            }
        }
        else
        {
            Debug.LogError("Can't read map configuration");
        }
            
        return tiles;
    }

    public static void Write()
    {
        StreamWriter stream = new StreamWriter(MAP_FOLDER + MAP_FILENAME + MAP_EXTENSION);

        stream.WriteLine("{");
        stream.WriteLine("\t\"maps\":");
        stream.WriteLine("\t[");

        for (int i = 0; i < LINES; i++)
        {
            string res = "\t\t";

            for (int j = 0; j < COLUMNS; j++)
            {
                res += "0";

                if (j < COLUMNS - 1)
                    res += ",";
            }

            if (i < LINES - 1)
                res += ",";

            stream.WriteLine(res);
        }

        stream.WriteLine("\t]");
        stream.WriteLine("}");
        stream.Flush();
    }
}