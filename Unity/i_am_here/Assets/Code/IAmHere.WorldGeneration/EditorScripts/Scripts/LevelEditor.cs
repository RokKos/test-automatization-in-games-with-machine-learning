using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace IAmHere.WorldGeneration
{
    public class LevelEditor : EditorWindow
    {

        public Levels inventoryItemList;
        private int viewIndex = 1;

        [MenuItem("Window/Level Editor %#e")]
        static void Init()
        {
            EditorWindow.GetWindow(typeof(LevelEditor));
        }

        void OnEnable()
        {
            if (EditorPrefs.HasKey("ObjectPath"))
            {
                string objectPath = EditorPrefs.GetString("ObjectPath");
                inventoryItemList = AssetDatabase.LoadAssetAtPath(objectPath, typeof(Levels)) as Levels;
            }

        }

        void OnGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Level Editor", EditorStyles.boldLabel);
            if (inventoryItemList != null)
            {
                if (GUILayout.Button("Show Levels List"))
                {
                    EditorUtility.FocusProjectWindow();
                    Selection.activeObject = inventoryItemList;
                }
            }

            if (GUILayout.Button("Open Level"))
            {
                OpenItemList();
            }

            if (GUILayout.Button("New Level"))
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = inventoryItemList;
            }

            GUILayout.EndHorizontal();

            if (inventoryItemList == null)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(10);
                if (GUILayout.Button("Create New Level", GUILayout.ExpandWidth(false)))
                {
                    CreateNewItemList();
                }

                if (GUILayout.Button("Open Existing Level", GUILayout.ExpandWidth(false)))
                {
                    OpenItemList();
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.Space(20);

            if (inventoryItemList != null)
            {
                GUILayout.BeginHorizontal();

                GUILayout.Space(10);

                if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false)))
                {
                    if (viewIndex > 1)
                        viewIndex--;
                }

                GUILayout.Space(5);
                if (GUILayout.Button("Next", GUILayout.ExpandWidth(false)))
                {
                    if (viewIndex < inventoryItemList.levels.Count)
                    {
                        viewIndex++;
                    }
                }

                GUILayout.Space(60);

                if (GUILayout.Button("Add Level", GUILayout.ExpandWidth(false)))
                {
                    AddItem();
                }

                if (GUILayout.Button("Delete Level", GUILayout.ExpandWidth(false)))
                {
                    DeleteItem(viewIndex - 1);
                }

                GUILayout.EndHorizontal();
                if (inventoryItemList.levels == null)
                    Debug.Log("NO LEVELS");
                if (inventoryItemList.levels.Count > 0)
                {
                    GUILayout.BeginHorizontal();
                    viewIndex = Mathf.Clamp(
                        EditorGUILayout.IntField("Current Level", viewIndex, GUILayout.ExpandWidth(false)), 1,
                        inventoryItemList.levels.Count);
                    //Mathf.Clamp (viewIndex, 1, inventoryItemList.itemList.Count);
                    EditorGUILayout.LabelField("of   " + inventoryItemList.levels.Count.ToString() + "  Levels", "",
                        GUILayout.ExpandWidth(false));
                    GUILayout.EndHorizontal();

                    Level level = inventoryItemList.levels[viewIndex - 1];

                    level.rows = EditorGUILayout.IntField("Rows", level.rows);
                    level.columns = EditorGUILayout.IntField("Columns", level.columns);
                    if (level.board == null || level.board.Length == 0 ||
                        level.board.Length != level.rows * level.columns)
                    {
                        level.board = new Square[level.rows * level.columns];
                        for (int y = 0; y < level.rows; ++y)
                        {
                            
                            for (int x = 0; x < level.columns; ++x)
                            {
                                int index = WorldManager.GetGridIndex(level.columns, y, x);
                                if (y == 0 || y == level.rows - 1 || x == 0 || x == level.columns - 1)
                                {
                                    level.board[index] = Square.kWall;
                                }
                            }
                        }
                    }

                    bool isDirty = false;
                    
                    EditorGUILayout.BeginHorizontal();
                    for (int x = 0; x < level.columns; ++x)
                    {
                        GUILayout.BeginVertical();
                        for (int y = 0; y < level.rows; ++y)
                        {
                            EditorGUILayout.BeginHorizontal();
                            int index = WorldManager.GetGridIndex(level.columns, y, x);
                            Square editorValue = (Square) EditorGUILayout.EnumPopup(level.board[index]);
                            
                            if (level.board[index] != editorValue)
                            {
                                isDirty = true;
                            }

                            level.board[index] = editorValue;
                            EditorGUILayout.EndHorizontal();
                        }

                        GUILayout.EndVertical();
                    }

                    EditorGUILayout.EndHorizontal();
                    GUILayout.Space(10);

                    if (isDirty)
                    {
                        EditorUtility.SetDirty(level);
                        AssetDatabase.SaveAssets();
                    }

                }
                else
                {
                    GUILayout.Label("This Levels are Empty.");
                }
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(inventoryItemList);
                AssetDatabase.SaveAssets();
            }
        }

        void CreateNewItemList()
        {
            // There is no overwrite protection here!
            // There is No "Are you sure you want to overwrite your existing object?" if it exists.
            // This should probably get a string from the user to create a new name and pass it ...
            viewIndex = 1;
            inventoryItemList = CreateNewLevel.Create();
            if (inventoryItemList)
            {
                inventoryItemList.levels = new List<Level>();
                string relPath = AssetDatabase.GetAssetPath(inventoryItemList);
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }

        void OpenItemList()
        {
            string absPath = EditorUtility.OpenFilePanel("Select Level", "", "");
            if (absPath.StartsWith(Application.dataPath))
            {
                string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
                inventoryItemList = AssetDatabase.LoadAssetAtPath(relPath, typeof(Levels)) as Levels;
                if (inventoryItemList.levels == null)
                    inventoryItemList.levels = new List<Level>();
                if (inventoryItemList)
                {
                    EditorPrefs.SetString("ObjectPath", relPath);
                }
            }
        }

        void AddItem()
        {
            Level newItem = new Level();
            newItem.rows = 0;
            newItem.columns = 0;
            newItem.board = null;
            inventoryItemList.levels.Add(newItem);
            viewIndex = inventoryItemList.levels.Count;
        }

        void DeleteItem(int index)
        {
            inventoryItemList.levels.RemoveAt(index);
        }
    }
}