using System;
using System.Collections.Generic;
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
        [Title("Prefab Data")]
        [AssetsOnly] [SerializeField] private GameObject m_platformPrefab;
        [AssetsOnly] [SerializeField] private GameObject m_ladderPrefab;

        [Title("Scene Data")] 
        [SceneObjectsOnly] [SerializeField] private Transform m_platformParent;

        [Title("Spawn Data")]
        [SerializeField] private List<Vector3> m_raycastDirections;
        
        [Title("Spawn Restrictions")]
        [SerializeField] private int m_maxTryCount;
        [SerializeField] private int m_platformCount;
        [Space]
        [SerializeField] private Range m_widthRange;
        [SerializeField] private Range m_heightRange;
        [Space]
        [SerializeField] private ScreenRange m_xScreenRestriction;
        [SerializeField] private ScreenRange m_yScreenRestriction;

        [Title("Ladder Restrictions")] 
        [SerializeField] private int m_ladderRayCount;
        
        [Title("Debug")]
        [SerializeField] private bool m_debugToggle;
        [ShowIf("m_debugToggle")] [SerializeField] private float m_debugRayDuration = 10f;
        [Title("Data")]
        [ShowInInspector] [SerializeField] [ReadOnly] private int m_tryCount;
        [ShowInInspector] [SerializeField] [ReadOnly] private Range m_worldXRange;
        [ShowInInspector] [SerializeField] [ReadOnly] private Range m_worldYRange;
        [Title("Prefab Data")]
        [ShowInInspector] [SerializeField] [ReadOnly] private GameObject m_selectedPlatform;
        [ShowInInspector] [SerializeField] [ReadOnly] private Vector2 m_selectedPlatformOffset;

        [Title("Scene Data")] 
        [ShowInInspector] [SerializeField] [ReadOnly] private List<GameObject> m_activePlatforms;

        private GameObject[] m_platforms;
        private void Awake()
        {
            GenerateWorldLimits();
            GenerateLevel();
            BuildLadders();
            
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
            m_platforms = new GameObject[m_platformCount];
            m_selectedPlatform = m_platformPrefab;
            GenerateSpriteOffset();
            for (var i = 0; i < m_platformCount; i++)
            {
                m_activePlatforms.Add(GeneratePlatform());
            }
        }

        /// <summary>
        /// Generates A Single Platform
        /// </summary>
        private GameObject GeneratePlatform()
        {
            var position = GeneratePosition();


            if (position == default(Vector3)) return null;
            
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
        private Vector3 GeneratePosition()
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


                Vector3 newPosition = new Vector3(xPosition, yPosition);

                if (RayCastAround(newPosition, yRange)) continue;
                
                return newPosition;
            }
            
            Debug.Log("Ran Out Of Tries");
            return default(Vector3);
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
                Debug.Log(hit.Length);
                if (hit.Length >= 1)
                {
                    Debug.Log("Collider HIT");
                    m_tryCount++;
                    status = true;
                    break;
                }
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
            var positions = GetRayXPositions(m_activePlatforms[0]);

            foreach (var position in positions)
            {
                Debug.Log(position);
                Debug.DrawRay(position, Vector3.down * 500, Color.green, 500f);
            }
        }

        private IEnumerable<Vector3> GetRayXPositions(GameObject p_platform)
        {
            var positions = new Vector3[m_ladderRayCount];
            var gameObjectPosition = p_platform.transform.position;
            var currentPosition = gameObjectPosition.x - m_selectedPlatformOffset.x;
            var widthOffset = m_selectedPlatformOffset.x * 2 / 5;

            for (var i = 0; i < m_ladderRayCount; i++)
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
