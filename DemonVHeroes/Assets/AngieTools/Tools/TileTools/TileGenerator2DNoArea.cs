using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEditor;
using System.IO;

namespace AngieTools
{
    using Tiles;

#if UNITY_EDITOR
    public class TileGenerator2DNoArea : EditorWindow
    {

        //User Input
        public GameObject m_groundParentObject;
        public GameObject m_tilesParentObject;
        public GameObject m_tilePrefab;

        //Data Vectors
        public Vector2 m_desiredTileGrid;
        public Vector3 m_desiredScale;
        public Vector3 m_desiredTileRotation;
        public Vector3 m_desiredGroundOffset;

        public Vector3 m_requiredOffset;

        private bool m_tileGridToggle = true;

        public Material m_debugMaterial;

        [MenuItem("AngieTools/TileGeneratorNoArea")]

        public static void ShowWindow()
        {
            TileGenerator2DNoArea t = EditorWindow.GetWindow<TileGenerator2DNoArea>();
        }

        #region Tool Logic
        void OnGUI()
        {
            TileGeneratorUsingArea();
        }

        private void TileGeneratorUsingArea()
        {
            EditorGUILayout.LabelField("Level Data");
            EditorGUI.indentLevel++;
            m_groundParentObject = EditorGUILayout.ObjectField("Ground Parent", m_groundParentObject, typeof(GameObject), true) as GameObject;
            m_desiredGroundOffset = EditorGUILayout.Vector3Field("Ground Offset", m_desiredGroundOffset);
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Prefab Data");
            EditorGUI.indentLevel++;
            m_tilePrefab = EditorGUILayout.ObjectField("Tile Prefab", m_tilePrefab, typeof(GameObject), true) as GameObject;
            m_tilesParentObject = EditorGUILayout.ObjectField("Tile Parent", m_tilesParentObject, typeof(GameObject), true) as GameObject;
            m_desiredTileRotation = EditorGUILayout.Vector3Field("Desired Tile Rotation", m_desiredTileRotation);
            m_desiredScale = EditorGUILayout.Vector3Field("Desired Tile Scale", m_desiredScale);
            EditorGUI.indentLevel--;

            if (m_tilePrefab != null)
            {
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                TileGridInput();
            }
        }


        //grab the grid input from the user
        private void TileGridInput()
        {
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            m_tileGridToggle = EditorGUILayout.Foldout(m_tileGridToggle, "Tile Grid");
            //Display Data
            if (m_tileGridToggle)
            {
                EditorGUI.indentLevel++;

                m_desiredTileGrid = EditorGUILayout.Vector2Field("Desired Grid Size", m_desiredTileGrid);
                EditorGUI.indentLevel--;

                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (GUILayout.Button("Spawn Tiles"))
            {
                CalculateRequiredSize();

                GameObject loadedPrefabData = PrefabUtility.LoadPrefabContents(AssetDatabase.GetAssetPath(m_tilePrefab));
                if (loadedPrefabData.GetComponent<PolygonCollider2D>() != null)
                {
                    SpawnAllTiles();
                }
                else
                {
                    SpawnAllTilesSquare();
                }
            }

        }

        private void CalculateRequiredSize()
        {
            GameObject loadedPrefabData = PrefabUtility.LoadPrefabContents(AssetDatabase.GetAssetPath(m_tilePrefab));
            PolygonCollider2D pc = loadedPrefabData.GetComponent<PolygonCollider2D>();

            if (pc == null)
            {
                BoxCollider2D bc = loadedPrefabData.GetComponent<BoxCollider2D>();
                m_requiredOffset.x = bc.bounds.extents.x;
                m_requiredOffset.y = bc.bounds.extents.y;
            }
            else
            {
                Debug.Log("Calculated Offset");
                Debug.Log("Required Offset: " + m_requiredOffset);
                m_requiredOffset.x = pc.bounds.extents.x;
                m_requiredOffset.y = pc.bounds.extents.y;
            }

        }

        private void SpawnAllTilesSquare()
        {
            float columnSwitch = 1;

            //temporary list to hold all the tiles
            List<List<BaseTileController>> allTiles = new List<List<BaseTileController>>();

            //Create The Tile Parent Object
            if (m_tilesParentObject != null)
            {
                DestroyImmediate(m_tilesParentObject);
                m_tilesParentObject = new GameObject("Tiles");
                m_tilesParentObject.transform.SetParent(m_groundParentObject.transform);
                m_tilesParentObject.AddComponent<TileParentController>();
            }
            else
            {
                m_tilesParentObject = new GameObject("Tiles");
                m_tilesParentObject.transform.SetParent(m_groundParentObject.transform);
                m_tilesParentObject.AddComponent<TileParentController>();
            }

            //Loop over the columns
            for (int i = 0; i < m_desiredTileGrid.y; i++)
            {
                columnSwitch *= -1;
                float sideSwitch = 1;

                //temporary list to hold 1 column of tiles
                List<BaseTileController> tiles = new List<BaseTileController>();

                //Loop over each tile in each column
                for (int z = 0; z < m_desiredTileGrid.x; z++)
                {

                    GameObject newTile = Instantiate(m_tilePrefab, new Vector3(z * (m_requiredOffset.x * m_desiredScale.x * 2), (i * (m_desiredScale.y * m_requiredOffset.y * 2)), 0), Quaternion.Euler(m_desiredTileRotation), m_tilesParentObject.transform);
                    newTile.transform.localScale = m_desiredScale;

                    //if we are debugging change the material to the debug material
                    if ((sideSwitch * columnSwitch) == 1)
                    {
                        newTile.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 0, 0, 1f);
                    }
                    else
                    {
                        newTile.GetComponentInChildren<SpriteRenderer>().color = new Color(0, 0, 1, 1f);
                    }

                    //every loop swap the side to give a checkerboard effect tot he grid while debugging
                    sideSwitch *= -1;

                    BaseTileController newTileController = newTile.GetComponent<BaseTileController>();

                    newTileController.Data.ColumnID = i;
                    newTileController.Data.RowID = z;

                    if (newTileController != null)
                    {
                        tiles.Add(newTileController);
                    }
                }

                allTiles.Add(tiles);
            }

            LinkTiles(allTiles);

            m_tilesParentObject.transform.localPosition = m_desiredGroundOffset;
        }

        //Spawns All THe Tiles
        private void SpawnAllTiles()
        {
            float columnSwitch = 1;

            //temporary list to hold all the tiles
            List<List<BaseTileController>> allTiles = new List<List<BaseTileController>>();

            //Create The Tile Parent Object
            if(m_tilesParentObject != null)
            {
                DestroyImmediate(m_tilesParentObject);
                m_tilesParentObject = new GameObject("Tiles");
                m_tilesParentObject.transform.SetParent(m_groundParentObject.transform);
            }
            else
            {
                m_tilesParentObject = new GameObject("Tiles");
                m_tilesParentObject.transform.SetParent(m_groundParentObject.transform);
            }

            //Loop over the columns
            for (int i = 0; i < m_desiredTileGrid.y; i++)
            {
                columnSwitch *= -1;
                float sideSwitch = 1;

                float columnXOffset = (i * (m_requiredOffset.x * m_desiredScale.x  + (m_requiredOffset.x * m_desiredScale.x / 1.525f))); 

                //temporary list to hold 1 column of tiles
                List<BaseTileController> tiles = new List<BaseTileController>();

                //Loop over each tile in each column
                for(int z = 0; z < m_desiredTileGrid.x; z++)
                {

                    GameObject newTile = Instantiate(m_tilePrefab, new Vector3((z * ((m_requiredOffset.x * m_desiredScale.x) + (m_requiredOffset.x * m_desiredScale.x / 5.7f)) + (columnXOffset / 2)), (i * (m_desiredScale.y * m_requiredOffset.y * 2)), 0), Quaternion.Euler(m_desiredTileRotation), m_tilesParentObject.transform);
                    newTile.transform.localScale = m_desiredScale;

                    //if we are debugging change the material to the debug material
                    if ((sideSwitch * columnSwitch) == 1)
                    {
                        newTile.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 0, 0, 0.3f);
                    }
                    else
                    {
                        newTile.GetComponentInChildren<SpriteRenderer>().color = new Color(0, 0, 1, 0.3f);
                    }

                    //every loop swap the side to give a checkerboard effect tot he grid while debugging
                    sideSwitch *= -1;

                    BaseTileController newTileController = newTile.GetComponent<BaseTileController>();
                    if(newTileController != null)
                    {
                        tiles.Add(newTileController);
                    }
                }

                allTiles.Add(tiles);
            }

            LinkTiles(allTiles);

            m_tilesParentObject.transform.localPosition = m_desiredGroundOffset;
        }



        private void LinkTiles(List<List<BaseTileController>> p_listData)
        {
            LinkTilesVertically(p_listData);
            LinkTilesHorizontally(p_listData);
        }

        private void LinkTilesVertically(List<List<BaseTileController>> p_listData)
        {
            for(int x = 0; x < m_desiredTileGrid.x; x++)
            { 
                for (int i = 0; i < m_desiredTileGrid.y; i++)
                {
                    if (i != p_listData.Count - 1)
                    {
                        p_listData[i][x].Data.TopTile = p_listData[i + 1][x];
                        p_listData[i + 1][x].Data.BottomTile = p_listData[i][x];
                    }
                }
            }
        }

        private void LinkTilesHorizontally(List<List<BaseTileController>> p_listData)
        {
            for (int x = 0; x < m_desiredTileGrid.y; x++)
            {
                for (int i = 0; i < m_desiredTileGrid.x; i++)
                {
                    if (i != p_listData[x].Count - 1)
                    {
                        p_listData[x][i].Data.RightTile = p_listData[x][i + 1];
                        p_listData[x][i + 1].Data.LeftTile = p_listData[x][i];
                    }
                }
            }
        }

        #endregion
    }
    #endif
}
