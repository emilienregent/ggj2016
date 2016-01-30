using System;

namespace Type
{
    public class MapConfiguration
    {
        public static string FOLDER    = "Assets/Resources/";
        public static string FILENAME  = "map";
        public static string EXTENSION = ".json";

        public static string PATH { get { return FOLDER + FILENAME + EXTENSION; } }

        public MapConfiguration()
        {
        }
    }
}

