using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using Type;
using Helper.Parser;
using Helper.Writer;
using System.IO;

namespace Tools 
{
    public class MapTool : EditorWindow
    {
        private List<Map>   _maps           = new List<Map>();
        private List<bool>  _foldouts       = new List<bool>();
        private bool        _isInitialized  = false;
        private Vector2     _scrollPosition = new Vector2();

        private static EditorWindow _window = null;

        [MenuItem ("Tools/Maps")]
        public static void  ShowWindow () 
        {
            _window = EditorWindow.GetWindow(typeof(MapTool));

            _window.maxSize = new Vector2(600f, 400f);
            _window.minSize = _window.maxSize;
        }
            
        private void OnGUI () {

            if (File.Exists(MapConfiguration.PATH.Replace(MapConfiguration.EXTENSION, ".lock")))
            {
                EditorGUILayout.LabelField("File is locked");

                if (GUILayout.Button("Force Unlock"))
                {
                    File.Delete(MapConfiguration.PATH.Replace(MapConfiguration.EXTENSION, ".lock"));
                }

                return;
            }
                
            if (_isInitialized == false)
            {
                _maps           = MapParser.Parse();
                _isInitialized  = true;
            }

            while (_foldouts.Count < _maps.Count)
            {
                _foldouts.Add(false);
            }

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.Width (600f), GUILayout.Height (350f));

            for (int i = 0; i < _maps.Count; i++)
            {
                _foldouts[i] = EditorGUILayout.Foldout(_foldouts[i], "Map " + (i + 1));

                if (_foldouts[i] == true)
                {
                    _maps[i].lines      = EditorGUILayout.IntField("Lines", _maps[i].lines);
                    _maps[i].columns    = EditorGUILayout.IntField("Columns", _maps[i].columns);

                    while (_maps[i].tiles.Count < _maps[i].lines * _maps[i].columns)
                    {
                        _maps[i].tiles.Add(new Tile(0));
                    }

                    for (int y = 0; y < _maps[i].lines; y++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        for (int x = 0; x < _maps[i].columns; x++)
                        {
                            EditorGUILayout.BeginVertical();

                            int         index   = _maps[i].GetIndexFromPosition(x, y);
                            TileType    type    = (TileType) EditorGUILayout.EnumPopup((TileType) _maps[i].tiles[index].type);

                            EditorGUILayout.BeginHorizontal();
                            GUI.enabled = type >= TileType.MINION_GREEN && type <= TileType.MINION_YELLOW;
                            int quantity = EditorGUILayout.IntField(_maps[i].tiles[index].quantity);
                            GUI.enabled = true;
                            EditorGUILayout.EndHorizontal();

                            _maps[i].tiles[index].type = (int)type;
                            _maps[i].tiles[index].quantity = Mathf.Max(Mathf.Min(quantity, 5), 1);

                            EditorGUILayout.EndVertical();
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }

            EditorGUILayout.EndScrollView();

            if (GUILayout.Button("Save"))
            {
                MapWriter.Write(_maps);
            }
        }
    }
}