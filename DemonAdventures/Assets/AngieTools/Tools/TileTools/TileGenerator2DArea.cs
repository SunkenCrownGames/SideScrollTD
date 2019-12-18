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
    public class TileGenerator2DArea : EditorWindow
    {

        //User Input
        public GameObject m_groundParentObject;
        public GameObject m_groundAreaObject;
        public GameObject m_tileAreaObject;
        public GameObject m_tilesParentObject;

        public GameObject m_tilePrefab;

        public GameObject m_tilePrefab11;
        public GameObject m_tilePrefab12;
        public GameObject m_tilePrefab13;

        public GameObject m_tilePrefab21;
        public GameObject m_tilePrefab22;
        public GameObject m_tilePrefab23;

        private List<GameObject> m_fisrtTileVariatons;
        private List<GameObject> m_secondTileVariations;

        //Data Vectors
        public Vector2 m_desiredTileGrid;
        public Vector3 m_requiredScale;
        public Vector3 m_requiredSize;
        public Vector3 m_desiredTileRotation;
        public Vector3 m_desiredGroundOffset;

        private bool m_groundDimensionsToggle = true;
        private bool m_tileDimensionsToggle = true;
        private bool m_tileGridToggle = true;


        private Vector2 m_areaSize;
        private Vector2 m_tileSize;

        private float m_maxHorizontalNativeTiles;
        private float m_maxVerticalNativeTiles;

        public Material m_debugMaterial;

        [MenuItem("AngieTools/TileGeneratorArea")]

        public static void ShowWindow()
        {
            TileGenerator2DArea t = EditorWindow.GetWindow<TileGenerator2DArea>();
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
            m_groundAreaObject = EditorGUILayout.ObjectField("Ground Area", m_groundAreaObject, typeof(GameObject), true) as GameObject;
            m_tileAreaObject = EditorGUILayout.ObjectField("Tile Area", m_tileAreaObject, typeof(GameObject), true) as GameObject;
            m_desiredGroundOffset = EditorGUILayout.Vector3Field("Ground Offset", m_desiredGroundOffset);
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Prefab Data");
            EditorGUI.indentLevel++;
            m_tilePrefab = EditorGUILayout.ObjectField("Tile Prefab", m_tilePrefab, typeof(GameObject), true) as GameObject;
            EditorGUI.indentLevel--;
            EditorGUILayout.LabelField("Prefab Variation Data");
            EditorGUI.indentLevel++;

            m_tilePrefab11 = EditorGUILayout.ObjectField("Tile Prefab Type 1 Variation 1", m_tilePrefab11, typeof(GameObject), true) as GameObject;
            m_tilePrefab12 = EditorGUILayout.ObjectField("Tile Prefab Type 1 Variation 2", m_tilePrefab12, typeof(GameObject), true) as GameObject;
            m_tilePrefab13 = EditorGUILayout.ObjectField("Tile Prefab Type 1 Variation 3", m_tilePrefab13, typeof(GameObject), true) as GameObject;

            EditorGUILayout.Space();

            m_tilePrefab21 = EditorGUILayout.ObjectField("Tile Prefab Type 2 Variation 1", m_tilePrefab21, typeof(GameObject), true) as GameObject;
            m_tilePrefab22 = EditorGUILayout.ObjectField("Tile Prefab Type 2 Variation 2", m_tilePrefab22, typeof(GameObject), true) as GameObject;
            m_tilePrefab23 = EditorGUILayout.ObjectField("Tile Prefab Type 2 Variation 3", m_tilePrefab23, typeof(GameObject), true) as GameObject;

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUI.indentLevel--;
            m_tilesParentObject = EditorGUILayout.ObjectField("Tile Parent", m_tilesParentObject, typeof(GameObject), true) as GameObject;
            m_desiredTileRotation = EditorGUILayout.Vector3Field("Desired Tile Rotation", m_desiredTileRotation);   
            EditorGUI.indentLevel--;

            if (m_tileAreaObject != null || m_groundAreaObject != null)
            {
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                GetAreaDimensions();
            }

            if (m_tilePrefab != null)
            {
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                GetTileDimensions();

                if ((m_tileAreaObject != null || m_groundAreaObject != null))
                {
                    TileGridInput();
                }
            }
        }

        //Get the dimensions of the area
        private void GetAreaDimensions()
        {
            GameObject groundTile = (m_tileAreaObject == null) ? m_groundAreaObject : m_tileAreaObject;
            string groundType = "";

            if(groundTile == m_groundAreaObject)
            {
                groundType = "Ground Area Dimensions";
            }
            else
            {
                groundType = "Tile Area Dimensions";
            }

            m_groundDimensionsToggle = EditorGUILayout.Foldout(m_groundDimensionsToggle, groundType);

            if (m_groundDimensionsToggle)
            {
                SpriteRenderer renderer = groundTile.GetComponentInChildren<SpriteRenderer>();

                if(renderer == null)
                {
                    renderer = groundTile.GetComponent<SpriteRenderer>();
                }

                float horizontalLength = renderer.bounds.size.x;
                float verticalLength = renderer.bounds.size.y;
                float height = renderer.bounds.size.y;

                m_areaSize.x = horizontalLength;
                m_areaSize.y = verticalLength;

                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField("Horizontal Length: " + horizontalLength);
                EditorGUILayout.LabelField("Vertical Length: " + verticalLength);
                EditorGUILayout.LabelField("height: " + height);
                EditorGUI.indentLevel--;
            }
        }


        //Get The Tile Dimensions from the prefab
        private void GetTileDimensions()
        {
            m_tileDimensionsToggle = EditorGUILayout.Foldout(m_tileDimensionsToggle, "Tile Dimensions");

            if(m_tileDimensionsToggle)
            {
                SpriteRenderer renderer = m_tilePrefab.GetComponentInChildren<SpriteRenderer>();

                if (renderer != null)
                {
                    float horizontalLength = renderer.bounds.size.x;
                    float verticalLength = renderer.bounds.size.y;
                    float height = renderer.bounds.size.z;

                    m_tileSize.x = horizontalLength;
                    m_tileSize.y = verticalLength;

                    EditorGUI.indentLevel++;
                    EditorGUILayout.LabelField("Horizontal Tile Length: " + horizontalLength);
                    EditorGUILayout.LabelField("Vertical Tile Length: " + verticalLength);
                    EditorGUI.indentLevel--;
                }
            }
        }

        //grab the grid input from the user
        private void TileGridInput()
        {
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            m_tileGridToggle = EditorGUILayout.Foldout(m_tileGridToggle, "Tile Grid");
            CalculateMaxNativeTiles();

            //Display Data
            if (m_tileGridToggle)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField("Max Horizontal Native Size Tile Count: " + m_maxHorizontalNativeTiles);
                EditorGUILayout.LabelField("Max Vertical Native Size Tile Count: " + m_maxVerticalNativeTiles);
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                m_desiredTileGrid = EditorGUILayout.Vector2Field("Desired Grid Size", m_desiredTileGrid);
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                CalculateRequiredSize();
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Required Tile Size Horizontal: " + m_requiredSize.x);
                EditorGUILayout.LabelField("Required Tile Size Vertical: " + m_requiredSize.y);
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                CalculateRequiredScale();
                EditorGUILayout.LabelField("Required Tile Scale Horizontal: " + m_requiredScale.x);
                EditorGUILayout.LabelField("Required Tile Scale Vertical: " + m_requiredScale.y);
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
                if(m_fisrtTileVariatons == null)
                {
                    m_fisrtTileVariatons = new List<GameObject>();
                }

                if (m_secondTileVariations == null)
                {
                    m_secondTileVariations = new List<GameObject>();
                }

                m_fisrtTileVariatons.Clear();

                m_fisrtTileVariatons.Add(m_tilePrefab11);
                m_fisrtTileVariatons.Add(m_tilePrefab12);
                m_fisrtTileVariatons.Add(m_tilePrefab13);

                m_secondTileVariations.Clear();

                m_secondTileVariations.Add(m_tilePrefab21);
                m_secondTileVariations.Add(m_tilePrefab22);
                m_secondTileVariations.Add(m_tilePrefab23);

                SpawnAllTiles();
            }

        }

        //Calculate The Maximum Amounts of tiles it can fit at the native tile size
        private void CalculateMaxNativeTiles()
        {
            m_maxHorizontalNativeTiles = m_areaSize.x / m_tileSize.x;
            m_maxVerticalNativeTiles = m_areaSize.y / m_tileSize.y;
        }

        //Calculate The Pixel Size Of the Tile
        private void CalculateRequiredSize()
        {
            m_requiredSize = new Vector3();
            m_requiredSize.x = m_areaSize.x / m_desiredTileGrid.x ;
            m_requiredSize.y = m_areaSize.y / m_desiredTileGrid.y;
        }


        //Calculates The Required Unity Scale Of The Tile
        private void CalculateRequiredScale()
        {
            m_requiredScale.x = m_requiredSize.x / m_tileSize.x;
            m_requiredScale.y = m_requiredSize.y / m_tileSize.y;
            m_requiredScale.z = 1;
        }


        //Spawns All THe Tiles
        private void SpawnAllTiles()
        {
            GameObject groundTile = (m_tileAreaObject == null) ? m_groundAreaObject : m_tileAreaObject;

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

            //Correct the tile parent object to the center of all the tiles
            m_tilesParentObject.transform.position += new Vector3((-m_requiredSize.x / 2) + m_areaSize.x / 2, -(m_requiredSize.y / 2) + m_areaSize.y / 2, 0);

            //Loop over the columns
            for (int i = 0; i < m_desiredTileGrid.y; i++)
            {
                columnSwitch *= -1;
                float sideSwitch = 1;

                //temporary list to hold 1 column of tiles
                List<BaseTileController> tiles = new List<BaseTileController>();

                int sequenceID = 0;

                //Loop over each tile in each column
                for(int z = 0; z < m_desiredTileGrid.x; z++)
                {

                    float offset = z * m_requiredSize.x;

                    GameObject newTile = null;

                    //if we are debugging change the material to the debug material
                    if ((sideSwitch * columnSwitch) == 1)
                    {
                        if (m_fisrtTileVariatons.Count == 0)
                        {
                            newTile = Instantiate(m_tilePrefab, new Vector3(z * m_requiredSize.x, (i * m_requiredSize.y), 0), Quaternion.Euler(m_desiredTileRotation), m_tilesParentObject.transform);
                            newTile.GetComponentInChildren<SpriteRenderer>().color = Color.cyan;
                            newTile.transform.localScale = m_requiredScale;
                        }
                        else
                        {

                            Debug.Log("Sequence ID: " + sequenceID);

                            newTile = Instantiate(m_fisrtTileVariatons[sequenceID], new Vector3(z * m_requiredSize.x, (i * m_requiredSize.y), 0), Quaternion.Euler(m_desiredTileRotation), m_tilesParentObject.transform);
                            newTile.transform.localScale = m_requiredScale;
                        }
                    }
                    else
                    {
                        if (m_secondTileVariations.Count == 0)
                        {
                            newTile = Instantiate(m_tilePrefab, new Vector3(z * m_requiredSize.x, (i * m_requiredSize.y), 0), Quaternion.Euler(m_desiredTileRotation), m_tilesParentObject.transform);
                            newTile.transform.localScale = m_requiredScale;
                            newTile.GetComponentInChildren<SpriteRenderer>().color = Color.grey;
                        }
                        else
                        {
                            newTile = Instantiate(m_secondTileVariations[sequenceID], new Vector3(z * m_requiredSize.x, (i * m_requiredSize.y), 0), Quaternion.Euler(m_desiredTileRotation), m_tilesParentObject.transform);
                            newTile.transform.localScale = m_requiredScale;
                        }
                    }

                    if(sequenceID < m_fisrtTileVariatons.Count - 1)
                    {
                        sequenceID++;
                    }
                    else
                    {
                        sequenceID = 0;
                    }



                    //every loop swap the side to give a checkerboard effect tot he grid while debugging
                    sideSwitch *= -1;

                    BaseTileController newTileController = newTile.GetComponent<BaseTileController>();
                    if(newTileController != null)
                    {
                        newTileController.Data.ColumnID = i;
                        newTileController.Data.RowID = z;
                        tiles.Add(newTileController);
                    }
                }

                allTiles.Add(tiles);
            }

            //LinkTiles(allTiles);

            Vector3 offsetedParent = new Vector3();
            offsetedParent = groundTile.transform.position;

            m_tilesParentObject.transform.position = offsetedParent;
          
            m_tilesParentObject.transform.position += m_desiredGroundOffset;
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
