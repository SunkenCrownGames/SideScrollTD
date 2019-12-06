using System;
using System.Collections.Generic;
using System.Linq;
using AngieTools;
using AngieTools.Tools;
using AngieTools.V2Tools;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;
// ReSharper disable Unity.PreferNonAllocApi
// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace Level
{
    public class LevelGenerator : MonoBehaviour
    {
        [Title("Toggles")]
        [SerializeField] private bool m_ladderToggle;
        [SerializeField] private bool m_debugToggle;
     
        #region  Platform

        [Title("Platform Data")]
        [FoldoutGroup("Platform")] [AssetsOnly] [SerializeField] private GameObject m_platformPrefab = null;

        [Title("Scene Data")] 
        [FoldoutGroup("Platform")] [SerializeField] private int m_layerId = 0;
        [FoldoutGroup("Platform")] [SceneObjectsOnly] [SerializeField] private Transform m_platformParent = null;

        [Title("Spawn Data")]
        [FoldoutGroup("Platform")] [SerializeField] private List<Vector3> m_raycastDirections = null;
        
        [Title("Spawn Restrictions")]
        [FoldoutGroup("Platform")] [SerializeField] private int m_maxTryCount = 0;
        [FoldoutGroup("Platform")] [SerializeField] private int m_platformCount = 0;
        [Space]
        [FoldoutGroup("Platform")] [SerializeField] private Range m_widthRange = null;
        [FoldoutGroup("Platform")] [SerializeField] private Range m_heightRange = null;
        [Space]
        [FoldoutGroup("Platform")] [SerializeField] private ScreenRange m_xScreenRestriction = null;
        [FoldoutGroup("Platform")] [SerializeField] private ScreenRange m_yScreenRestriction = null;

        #endregion
        
        #region  Ladder
        
        [ShowIfGroup("m_ladderToggle")]
        [FoldoutGroup("m_ladderToggle/Ladder")] [Title("Ladder Data")]
        [FoldoutGroup("m_ladderToggle/Ladder")] [AssetsOnly] [SerializeField] private GameObject m_ladderPrefab;
        
        [Title("Ladder Restrictions")]
        [SerializeField] [FoldoutGroup("m_ladderToggle/Ladder")] private float m_ladderWidth;
        [SerializeField] [FoldoutGroup("m_ladderToggle/Ladder")] private float m_ladderBleedOffset;
        [SerializeField] [FoldoutGroup("m_ladderToggle/Ladder")] private int m_laddersToBreak;
        [SerializeField] [FoldoutGroup("m_ladderToggle/Ladder")] private int m_ladderRayCount;
        
        #endregion

        #region Debug
        
        [FoldoutGroup("Debug")]
        [ShowIf("m_debugToggle")] [SerializeField] private float m_debugRayDuration = 10f;
        [Title("Data")]
        [FoldoutGroup("Debug")] [ShowInInspector] [SerializeField] [ReadOnly] private int m_tryCount;
        [FoldoutGroup("Debug")] [ShowInInspector] [SerializeField] [ReadOnly] private Range m_worldXRange;
        [FoldoutGroup("Debug")] [ShowInInspector] [SerializeField] [ReadOnly] private Range m_worldYRange;
        [Title("Prefab Data")]
        [FoldoutGroup("Debug")] [ShowInInspector] [SerializeField] [ReadOnly] private GameObject m_selectedPlatform;
        [FoldoutGroup("Debug")] [ShowInInspector] [SerializeField] [ReadOnly] private Vector2 m_selectedPlatformOffset;

        [Title("Scene Data")] 
        [FoldoutGroup("Debug")] [ShowInInspector] [SerializeField] [ReadOnly] private List<Platform> m_activePlatforms;

        private static LevelGenerator _instance = null;
        private GameObject[] m_platforms;
        
        #endregion
        private void Awake()
        {
            BindInstance();
            GenerateWorldLimits();
            SetupLevelGenerator();
            GenerateLevel();
            
            if(m_ladderToggle)
                BuildLadders();
            
        }

        private void BindInstance()
        {
            if (_instance != null) Destroy(gameObject);

            _instance = this;
        }

        private void SetupLevelGenerator()
        {
            m_platforms = new GameObject[m_platformCount];
            m_selectedPlatform = m_platformPrefab;
            GenerateSpriteOffset();
        }

        
        #region Step 1 Generate Platforms
        /// <summary>
        /// Generate World Limits from the Viewport
        /// </summary>
        private void GenerateWorldLimits()
        {
            m_worldXRange = new Range();
            m_worldYRange = new Range();

            var maximumWorldPosition =
                WorldUtils.ViewportToWorld(m_xScreenRestriction.MaxValue, m_yScreenRestriction.MaxValue);
            
            var minimumWorldPosition =
                WorldUtils.ViewportToWorld(m_xScreenRestriction.MinValue, m_yScreenRestriction.MinValue);

            m_worldXRange.StartValue = minimumWorldPosition.x;
            m_worldXRange.EndValue = maximumWorldPosition.x;
            
            m_worldYRange.StartValue = minimumWorldPosition.y;
            m_worldYRange.EndValue = maximumWorldPosition.y;
        }

        private void GenerateLevel()
        {
            for (var i = 0; i < m_platformCount; i++)
            {
                var platform = GeneratePlatform()?.GetComponent<Platform>();
                
                if(platform != null)
                    m_activePlatforms.Add(platform.GetComponent<Platform>());
            }
        }

        /// <summary>
        /// Generates A Single Platform
        /// </summary>
        [FoldoutGroup("Debug")] [Button("Generate Platform")]
        private GameObject GeneratePlatform()
        {
            var generated = GeneratePosition(out var position);


            if (!generated)
            {
                Debug.Log("Failled To Create Returning");
                return null;
            }
            
            Debug.Log($"Successfully created platform after {m_tryCount} Tries");
            GameObject spawnedPlatform = Instantiate(m_selectedPlatform, position, Quaternion.identity, m_platformParent);
            m_tryCount = 0;

            return spawnedPlatform;
        }

        /// <summary>
        /// Will GRab the Sprite Offsets
        /// </summary>
        private void GenerateSpriteOffset()
        {
            var sr = m_selectedPlatform.GetComponentInChildren<SpriteRenderer>();
            m_selectedPlatformOffset = sr.bounds.extents;
        }

        /// <summary>
        /// Will Try to generate a position within m_maxTries
        /// </summary>
        /// <returns> Returns the position that it will place the platform At</returns>
        private bool GeneratePosition(out Vector3 p_position)
        {
            while (m_tryCount < m_maxTryCount)
            {
                var xWorldRange = new Range(m_worldXRange);
                var yWorldRange = new Range(m_worldYRange);
                xWorldRange.StartValue += m_selectedPlatformOffset.x;
                xWorldRange.EndValue -= m_selectedPlatformOffset.x;

                yWorldRange.StartValue += m_selectedPlatformOffset.y;
                yWorldRange.EndValue += m_selectedPlatformOffset.y;

                var xPosition = WorldUtils.RandomRange(xWorldRange);
                var yPosition = WorldUtils.RandomRange(yWorldRange);

                var xRange = WorldUtils.RandomRange(m_widthRange);
                var yRange = WorldUtils.RandomRange(m_heightRange);


                var newPosition = new Vector3(xPosition, yPosition);

                if (RayCastAround(newPosition, yRange)) continue;

                p_position = newPosition;
                return true;
            }
            
            Debug.Log("Ran Out Of Tries");
            p_position = Vector3.zero;
            return false;
        }
        
        /// <summary>
        /// Fires Raycasts all around the platform
        /// </summary>
        /// <param name="p_startPosition"> The Center of the platform</param>
        /// <param name="p_yRange"> The range at which to fire the ray at</param>
        /// <returns></returns>
        private bool RayCastAround(Vector3 p_startPosition, float p_yRange)
        {
            var status = false;

            foreach (var direction in m_raycastDirections)
            {
                var hit = Physics2D.RaycastAll(p_startPosition, direction.normalized, p_yRange);

                if (hit.Length < 1) continue;
                
                Debug.Log("Collider HIT");
                m_tryCount++;
                status = true;
                break;
            }

            if (!m_debugToggle || status) return status;
            
            foreach (var direction in m_raycastDirections)
            {
                Debug.DrawRay(p_startPosition, direction.normalized * p_yRange, Color.red, m_debugRayDuration); 
            }

            return status;
        }
        #endregion

        #region Step 2 Build Ladders

        private void BuildLadders()
        {
            foreach (var platform in m_activePlatforms)
            {
                if (platform == null) return;
                
                platform.Link();
                
            }
            
            BreakLadders();
            
            GenerateLadder();
        }

        private void BreakLadders()
        {
            var availablePlatformsToBreak =
                m_activePlatforms.Where(p_platform => p_platform.LinkCount > 1).ToList();
            
            
            if (m_laddersToBreak > availablePlatformsToBreak.Count)
                m_laddersToBreak = availablePlatformsToBreak.Count;
            
            for (var i = 0; i < m_laddersToBreak; i++)
            {
                var randomPlatform = Random.Range(0, availablePlatformsToBreak.Count);
                var platform = availablePlatformsToBreak[randomPlatform];
                platform.BreakLadder();
                availablePlatformsToBreak.RemoveAt(randomPlatform);
            }
        }

        private void GenerateLadder()
        {
            var buildablePlatforms = m_activePlatforms.Where(p_platform => p_platform.LinkCount > 0).ToList();

            var spriteRenderer = m_ladderPrefab.GetComponent<SpriteRenderer>() == null
                ? m_ladderPrefab.GetComponentInChildren<SpriteRenderer>() : null;

            if (spriteRenderer == null) return;
            
            var sprite = spriteRenderer.sprite;

            foreach (var platform in buildablePlatforms)
            {
                foreach (var bottomLink in platform.BottomLink.Data)
                {
                    var link = Random.Range(0, bottomLink.m_rayStartPosition.Count);
                    var endPosition = bottomLink.m_rayStartPosition[link];
                    endPosition.y = bottomLink.m_hitPlatform.transform.position.y;
                    
                    var length = Mathf.Abs(endPosition.y - bottomLink.m_rayStartPosition[link].y);
                    var heightScale = length / (spriteRenderer.bounds.extents.y * 2);

                    GameObject ladder = Instantiate(m_ladderPrefab, bottomLink.m_rayStartPosition[link], Quaternion.identity,
                        platform.gameObject.transform);

                    ladder.transform.localScale = new Vector3(m_ladderWidth, heightScale, 1);
                }
            }
        }

        public static GameObject GetLadderPrefab()
        {
            return _instance == null ? null : _instance.m_ladderPrefab;
        }
        
        public static Vector3[] GetRayXPositions(GameObject p_platform, int p_ladderRayCount, float p_platformOffset)
        {
            var positions = new Vector3[p_ladderRayCount];
            var gameObjectPosition = p_platform.transform.position;
            var currentPosition = gameObjectPosition.x - p_platformOffset;
            var widthOffset = p_platformOffset * 2 / 5;

            for (var i = 0; i < p_ladderRayCount; i++)
            {
                positions[i].x = currentPosition;
                positions[i].y = gameObjectPosition.y;
                positions[i].z = gameObjectPosition.z;
                
                currentPosition += widthOffset;
            }

            return positions;
        }
        
        public static Vector3[] GetRayXPositions(GameObject p_platform)
        {
            var positions = new Vector3[_instance.m_ladderRayCount];
            var gameObjectPosition = p_platform.transform.position;
            var currentPosition = gameObjectPosition.x - _instance.m_selectedPlatformOffset.x + _instance.m_ladderBleedOffset;
            var widthOffset = (_instance.m_selectedPlatformOffset.x - _instance.m_ladderBleedOffset) * 2 / 5;

            for (var i = 0; i < _instance.m_ladderRayCount; i++)
            {
                positions[i].x = currentPosition;
                positions[i].y = gameObjectPosition.y;
                positions[i].z = gameObjectPosition.z;
                
                currentPosition += widthOffset;
            }

            return positions;
        }
        

        #endregion
        
    }
}
