using System;
using AngieTools;
using AngieTools.Tools;
using AngieTools.V2Tools;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level
{
    public class LevelGenerator : MonoBehaviour
    {
        [Title("Prefab Data")]
        [AssetsOnly] [SerializeField] private GameObject m_platformPrefab;
        [AssetsOnly] [SerializeField] private GameObject m_ladderPrefab;

        [Title("Scene Data")] 
        [SceneObjectsOnly] [SerializeField] private Transform m_platformParent;
        
        
        [Title("Spawn Restrictions")]
        [SerializeField] int m_maxTryCount;
        [SerializeField] int m_platformCount;
        [Space]
        [SerializeField] private Range m_widthRange;
        [SerializeField] private Range m_heightRange;
        [Space]
        [SerializeField] ScreenRange m_xScreenRestriction;
        [SerializeField] ScreenRange m_yScreenRestriction;

        [Title("Debug")] 
        [ShowInInspector] [SerializeField] [ReadOnly] private int m_tryCount;
        [ShowInInspector] [SerializeField] [ReadOnly] private Range m_worldXRange;
        [ShowInInspector] [SerializeField] [ReadOnly] private Range m_worldYRange;
        [ShowInInspector] [SerializeField] [ReadOnly] private GameObject m_selectedPlatform;
        [ShowInInspector] [SerializeField] [ReadOnly] private Vector2 m_selectedPlatformOffset;

        private PlatformRayData m_rayData;
        
        private GameObject[] m_platforms;
        private void Awake()
        {
            GenerateWorldLimits();
            GenerateLevel();
        }

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
                var position = GeneratePosition();
                if (m_tryCount >= m_maxTryCount)
                {
                    Debug.Log($"Count not create platform after {m_maxTryCount} Tries");
                    break;
                }

                Debug.Log($"Successfully created platform after {m_tryCount} Tries");
                GameObject spawnedPlatform = Instantiate(m_selectedPlatform, position, Quaternion.identity, m_platformParent);
                spawnedPlatform.GetComponent<Platform>().BindRayData(m_rayData);
                m_tryCount = 0;
            }
        }

        private void GenerateSpriteOffset()
        {
            var sr = m_selectedPlatform.GetComponentInChildren<SpriteRenderer>();
            m_selectedPlatformOffset = sr.bounds.extents;
        }

        private Vector3 GeneratePosition()
        {

            while (m_tryCount < m_maxTryCount)
            {

                var offsetX = WorldUtils.RandomRange(m_widthRange);
                var offsetY = WorldUtils.RandomRange(m_heightRange);

                var position = new Vector3();
                var initialXPosition = 0.0f;
                var initialYPosition = 0.0f;

                
                    //X RANGE
                    var xRange = new Range(m_worldXRange);

                    xRange.EndValue -= Mathf.Abs(m_selectedPlatformOffset.x + offsetX);
                    xRange.StartValue += m_selectedPlatformOffset.x + offsetX;

                    initialXPosition = WorldUtils.RandomRange(xRange);
                    //Y RANGE
                    var yRange = new Range(m_worldYRange);

                    yRange.EndValue -= Mathf.Abs(m_selectedPlatformOffset.y + offsetY);
                    yRange.StartValue += (m_selectedPlatformOffset.y + offsetY);
                    //Debug.Log("Y is invalid");
                    initialYPosition = WorldUtils.RandomRange(yRange);

                    m_rayData = GeneratePlatformRayData(initialXPosition, initialYPosition, offsetX, offsetY);
                
                if (CheckForOtherPlatformsInRange(m_rayData))
                {
                    //Debug.Log("Return True Here Connecting");
                    m_tryCount++;
                    continue;
                }

                position.x = initialXPosition;
                position.y = initialYPosition;
                
                return position;
            }

            return default(Vector3);
        }

        private PlatformRayData GeneratePlatformRayData(float p_initialXPosition, float p_initialYPosition, float p_offsetX, float p_offsetY)
        {
            var initialPosition = new Vector3(p_initialXPosition, p_initialYPosition);
            
            #region Left
            //LEFT
            var leftPosition = new Vector3(p_initialXPosition - m_selectedPlatformOffset.x, p_initialYPosition);
            //LEFT UP
            var leftUpPosition = new Vector3(p_initialXPosition - m_selectedPlatformOffset.x, p_initialYPosition + m_selectedPlatformOffset.y);
            //LEFT DOWN
            var leftDownPosition = new Vector3(p_initialXPosition - m_selectedPlatformOffset.x, p_initialYPosition - m_selectedPlatformOffset.y);
            #endregion
            
            #region Right
            //RIGHT
            var rightPosition = new Vector3(p_initialXPosition + m_selectedPlatformOffset.x, p_initialYPosition);
            //RIGHT UP
            var rightUpPosition = new Vector3(p_initialXPosition + m_selectedPlatformOffset.x, p_initialYPosition + m_selectedPlatformOffset.y);
            //RIGHT DOWN
            var rightDownPosition = new Vector3(p_initialXPosition + m_selectedPlatformOffset.x, p_initialYPosition - m_selectedPlatformOffset.y);
            #endregion
            
            #region Middle
            //MIDDLE
            var upPosition = new Vector3(p_initialXPosition, p_initialYPosition + m_selectedPlatformOffset.y);
            var downPosition = new Vector3(p_initialXPosition, p_initialYPosition - m_selectedPlatformOffset.y);
            #endregion
            
            return new PlatformRayData(leftPosition, leftUpPosition, leftDownPosition, rightPosition,
                rightUpPosition, rightDownPosition, upPosition, downPosition, p_offsetX, p_offsetY);
        }

        private bool CheckForOtherPlatformsInRange(PlatformRayData p_data)
        {

            #region RAYS
            #region LEFT
            //LEFT
            var inSightLeft = 
                Physics2D.Raycast(p_data.LeftPosition, Vector3.left, p_data.OffsetX);
            //LEFT UP
            var inSightUpLeft = 
                Physics2D.Raycast(p_data.LeftUpPosition, Vector3.up, p_data.OffsetY);
            //LEFT DOWN
            var inSightDownLeft = 
                Physics2D.Raycast(p_data.LeftDownPosition, Vector3.down, p_data.OffsetX);
            //LEFT DIAGONAL DOWN
            var inSightDiagonalDownLeft =
                Physics2D.Raycast(p_data.LeftDownPosition, VectorUtils.DiagonalDownLeft, p_data.OffsetY);
            //LEFT DIAGONAL UP
            var inSightDiagonalUpLeft = 
                Physics2D.Raycast(p_data.LeftUpPosition, VectorUtils.DiagonalUpLeft , p_data.OffsetY);
            #endregion

            #region  RIGHT
            //RIGHT
            var inSightRight 
                = Physics2D.Raycast(p_data.RightPosition, Vector3.right, p_data.OffsetX);
            //RIGHT UP
            var inSightUpRight 
                = Physics2D.Raycast(p_data.RightUpPosition, Vector3.up, p_data.OffsetY);
            //RIGHT DOWN
            var inSightDownRight 
                = Physics2D.Raycast(p_data.RightDownPosition, Vector3.down, p_data.OffsetX);
            //LEFT DIAGONAL DOWN
            var inSightDiagonalDownRight 
                = Physics2D.Raycast(p_data.RightDownPosition, VectorUtils.DiagonalDownRight , p_data.OffsetY); 
            //LEFT DIAGONAL UP
            var inSightDiagonalUpRight 
                = Physics2D.Raycast(p_data.RightUpPosition, VectorUtils.DiagonalUpRight , p_data.OffsetY);
            #endregion
            
            #region MIDDLE
            var inSightUp 
                = Physics2D.Raycast(p_data.UpPosition, Vector3.up, p_data.OffsetY);
            var inSightDown 
                = Physics2D.Raycast(p_data.DownPosition, Vector3.down, p_data.OffsetY);
            #endregion
            #endregion

            PlatformRayData.DrawRays(p_data);


            //Debug.Log($"Triggers: Left: {(bool)inSightLeft} Left Up: {(bool)inSightUpLeft} Left Down: {(bool)inSightDownLeft}");
            //Debug.Log($"Triggers: Right: {(bool)inSightRight} Right Up: {(bool)inSightUpRight} Right Down: {(bool)inSightDownRight}");
            //Debug.Log($"Triggers: Middle Up: {(bool)inSightUp} Midde Down: {(bool)inSightDown}");

            var diagonalChecks = inSightDiagonalDownLeft || inSightDiagonalDownRight || inSightDiagonalUpLeft ||
                                 inSightDiagonalUpRight;
            
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return inSightUpLeft || inSightDownLeft || inSightUpRight || inSightDownRight || inSightDown ||
                   inSightUp || inSightLeft || inSightRight || diagonalChecks;
        }
    }
}
