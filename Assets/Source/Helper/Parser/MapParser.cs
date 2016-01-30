using System;
using System.Collections.Generic;
using UnityEngine;
using Type;
using SimpleJSON;
using System.Collections;

namespace Helper.Parser
{
    public class MapParser
    {
        public static List<Map> Parse()
        {
            TextAsset   configuration   = Resources.Load(MapConfiguration.FILENAME) as TextAsset;
            List<Map>   maps            = new List<Map>();

            if (configuration != null)
            {
                JSONNode    json    = JSON.Parse(configuration.text);

                if (json != null)
                {
                    JSONArray nodes = json["maps"].AsArray;

                    for (int i = 0; i < nodes.Count; i++)
                    {
                        JSONNode node = json["maps"][i];
                        Map map = new Map(node["lines"].AsInt, node["columns"].AsInt);

                        for (int y = 0; y < map.lines; y++)
                        {
                            for (int x = 0; x < map.columns; x++)
                            {
                                int index = map.GetIndexFromPosition(x, y);
                                int type = node["tiles"][index].AsInt;

                                map.tiles.Add(new Tile(type));
                            }
                        }

                        maps.Add(map);
                    }
                }
            }

            if(maps.Count == 0)
            {
                Debug.LogError("Can't read map configuration");
            }

            return maps;
        }
    }
}