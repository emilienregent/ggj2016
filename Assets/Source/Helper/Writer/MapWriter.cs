using System;
using Type;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Helper.Writer
{
    public class MapWriter
    {
        public static void Write(List<Map> maps)
        {
            if (File.Exists(MapConfiguration.PATH.Replace(MapConfiguration.EXTENSION, ".lock")))
            {
                return;
            }

            File.WriteAllText(MapConfiguration.PATH.Replace(MapConfiguration.EXTENSION, ".lock"), "");

            StreamWriter stream = new StreamWriter(MapConfiguration.PATH, false);

            stream.WriteLine("{");
            stream.WriteLine("\t\"maps\":");
            stream.WriteLine("\t[");

            for (int i = 0; i < maps.Count; i++)
            {
                stream.WriteLine("\t\t{");
                stream.WriteLine("\t\t\t\"lines\" : " + maps[i].lines + ",");
                stream.WriteLine("\t\t\t\"columns\" : " + maps[i].columns + ",");
                stream.WriteLine("\t\t\t\"tiles\" : [");

                for (int y = 0; y < maps[i].lines; y++)
                {
                    string res = "\t\t\t\t";
                    
                    for (int x = 0; x < maps[i].columns; x++)
                    {
                        int index = maps[i].GetIndexFromPosition(x, y);

                        res += maps[i].tiles[index].type;
                        
                        if (x < maps[i].columns - 1)
                            res += ",";
                    }
                    
                    if (y < maps[i].lines - 1)
                        res += ",";
                    
                    stream.WriteLine(res);
                }
                stream.WriteLine("\t\t\t],");

                stream.WriteLine("\t\t\t\"quantities\" : [");

                for (int y = 0; y < maps[i].lines; y++)
                {
                    string res = "\t\t\t\t";

                    for (int x = 0; x < maps[i].columns; x++)
                    {
                        int index = maps[i].GetIndexFromPosition(x, y);

                        res += maps[i].tiles[index].quantity;

                        if (x < maps[i].columns - 1)
                            res += ",";
                    }

                    if (y < maps[i].lines - 1)
                        res += ",";

                    stream.WriteLine(res);
                }
                stream.WriteLine("\t\t\t]");

                if (i < maps.Count - 1)
                    stream.WriteLine("\t\t},");
                else
                    stream.WriteLine("\t\t}");
            }

            stream.WriteLine("\t]");
            stream.WriteLine("}");
            stream.Flush();
            stream.Close();

            AssetDatabase.ImportAsset(MapConfiguration.PATH);

            File.Delete(MapConfiguration.PATH.Replace(MapConfiguration.EXTENSION, ".lock"));
        }
    }
}