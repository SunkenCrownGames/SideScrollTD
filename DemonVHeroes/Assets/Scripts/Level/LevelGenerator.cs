using System.Collections.Generic;
using System.Linq;
using System.Text;
using AngieTools;
using AngieTools.Tools;
using AngieTools.V2Tools;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

// ReSharper disable Unity.PreferNonAllocApi
// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace Level
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private bool m_debugToggle;

        [Title("Toggles")] [SerializeField] private bool m_ladderToggle;

        #region  Pathing

        private List<List<PlatformPath>> m_paths;

        #endregion

        private void Awake()
        {
            BindInstance();
            GenerateWorldLimits();
            SetupLevelGenerator();
            GenerateLevel();

            if (m_ladderToggle)
            {
                BuildLadders();
                BuildPath();
            }
        }

        private void BindInstance()
        {
            if (_instance != null) Destroy(gameObject);

            _instance = this;
            m_paths = new List<List<PlatformPath>>();
        }

        private void SetupLevelGenerator()
        {
            m_platforms = new GameObject[m_platformCount];
            m_selectedPlatform = m_platformPrefab;
            GenerateSpriteOffset();
        }

        #region  Platform

        [Title("Platform Data")] [FoldoutGroup("Platform")] [AssetsOnly] [SerializeField]
        private GameObject m_platformPrefab;

        [Title("Scene Data")] [FoldoutGroup("Platform")] [SerializeField]
        private int m_layerId;

        [FoldoutGroup("Platform")] [SceneObjectsOnly] [SerializeField]
        private Transform m_platformParent;

        [Title("Spawn Data")] [FoldoutGroup("Platform")] [SerializeField]
        private List<Vector3> m_raycastDirections;

        [Title("Spawn Restrictions")] [FoldoutGroup("Platform")] [SerializeField]
        private int m_maxTryCount;

        [FoldoutGroup("Platform")] [SerializeField]
        private int m_platformCount;

        [Space] [FoldoutGroup("Platform")] [SerializeField]
        private Range m_widthRange;

        [FoldoutGroup("Platform")] [SerializeField]
        private Range m_heightRange;

        [Space] [FoldoutGroup("Platform")] [SerializeField]
        private ScreenRange m_xScreenRestriction;

        [FoldoutGroup("Platform")] [SerializeField]
        private ScreenRange m_yScreenRestriction;

        #endregion

        #region  Ladder

        [ShowIfGroup("m_ladderToggle")]
        [FoldoutGroup("m_ladderToggle/Ladder")]
        [Title("Ladder Data")]
        [FoldoutGroup("m_ladderToggle/Ladder")]
        [AssetsOnly]
        [SerializeField]
        private GameObject m_ladderPrefab;

        [Title("Ladder Restrictions")] [SerializeField] [FoldoutGroup("m_ladderToggle/Ladder")]
        private float m_ladderWidth;

        [SerializeField] [FoldoutGroup("m_ladderToggle/Ladder")]
        private float m_ladderBleedOffset;

        [SerializeField] [FoldoutGroup("m_ladderToggle/Ladder")]
        private int m_laddersToBreak;

        [SerializeField] [FoldoutGroup("m_ladderToggle/Ladder")]
        private int m_ladderRayCount;

        #endregion

        #region Debug

        [FoldoutGroup("Debug")] [ShowIf("m_debugToggle")] [SerializeField]
        private float m_debugRayDuration = 10f;

        [Title("Data")] [FoldoutGroup("Debug")] [ShowInInspector] [SerializeField] [ReadOnly]
        private int m_tryCount;

        [FoldoutGroup("Debug")] [ShowInInspector] [SerializeField] [ReadOnly]
        private Range m_worldXRange;

        [FoldoutGroup("Debug")] [ShowInInspector] [SerializeField] [ReadOnly]
        private Range m_worldYRange;

        [Title("Prefab Data")] [FoldoutGroup("Debug")] [ShowInInspector] [SerializeField] [ReadOnly]
        private GameObject m_selectedPlatform;

        [FoldoutGroup("Debug")] [ShowInInspector] [SerializeField] [ReadOnly]
        private Vector2 m_selectedPlatformOffset;

        [Title("Scene Data")] [FoldoutGroup("Debug")] [ShowInInspector] [SerializeField] [ReadOnly]
        private List<Platform> m_activePlatforms;

        private static LevelGenerator _instance;
        private GameObject[] m_platforms;

        #endregion


        #region Step 1 Generate Platforms

        /// <summary>
        ///     Generate World Limits from the Viewport
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

                if (platform != null)
                    m_activePlatforms.Add(platform.GetComponent<Platform>());
            }
        }

        /// <summary>
        ///     Generates A Single Platform
        /// </summary>
        [FoldoutGroup("Debug")]
        [Button("Generate Platform")]
        private GameObject GeneratePlatform()
        {
            var generated = GeneratePosition(out var position);


            if (!generated)
            {
                Debug.Log("Failled To Create Returning");
                return null;
            }

            Debug.Log($"Successfully created platform after {m_tryCount} Tries");
            var spawnedPlatform = Instantiate(m_selectedPlatform, position, Quaternion.identity, m_platformParent);
            m_tryCount = 0;

            return spawnedPlatform;
        }

        /// <summary>
        ///     Will GRab the Sprite Offsets
        /// </summary>
        private void GenerateSpriteOffset()
        {
            var sr = m_selectedPlatform.GetComponentInChildren<SpriteRenderer>();
            m_selectedPlatformOffset = sr.bounds.extents;
        }

        /// <summary>
        ///     Will Try to generate a position within m_maxTries
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
        ///     Fires Raycasts all around the platform
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
                Debug.DrawRay(p_startPosition, direction.normalized * p_yRange, Color.red, m_debugRayDuration);

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
                ? m_ladderPrefab.GetComponentInChildren<SpriteRenderer>()
                : null;

            if (spriteRenderer == null) return;

            var sprite = spriteRenderer.sprite;

            foreach (var platform in buildablePlatforms)
            foreach (var bottomLink in platform.BottomLink.Data)
            {
                var link = Random.Range(0, bottomLink.m_rayStartPosition.Count);
                var endPosition = bottomLink.m_rayStartPosition[link];
                endPosition.y = bottomLink.m_hitPlatform.transform.position.y;

                var length = Mathf.Abs(endPosition.y - bottomLink.m_rayStartPosition[link].y);
                var heightScale = length / (spriteRenderer.bounds.extents.y * 2);

                var ladder = Instantiate(m_ladderPrefab, bottomLink.m_rayStartPosition[link], Quaternion.identity,
                    platform.gameObject.transform);

                ladder.transform.localScale = new Vector3(m_ladderWidth, heightScale, 1);
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
            var currentPosition = gameObjectPosition.x - _instance.m_selectedPlatformOffset.x +
                                  _instance.m_ladderBleedOffset;
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

        #region Step 3 Build Pathing


        private void BuildPath()
        {
            //get all the nodes that we can path too including ground;
            var startNodes = GetPathingList();

            //run as long as we have a start node available in the list
            for (int i = 0; i < 2; i++)
            {
                //generate the final list
                var finalNodeList = GetPathingList();
                //generate a list of nodes to visit;
                var nodestoVisit = GetPathingList();
                //set the start node to the first node in the start node list
                var node = startNodes[i];
                //Remove said node from the list
                nodestoVisit.Remove(node);
                //Update the start node distance to 0
                node.UpdateDistance(0);
                
                var adjacentPlatformQueue = new Queue<PlatformPath>(); 

                //while an unvisted node is still present in the list keep looping
                while (adjacentPlatformQueue.Count > 0)
                {
                    //combine all links
                    var allHits = new HitPlatformResult();
                    allHits.Data.AddRange(node.Node.BottomLink.Data);
                    allHits.Data.AddRange(node.Node.TopLink.Data);
                    
                    foreach (var link in allHits.Data)
                    {
                        //calculate distance between nodes
                        var distance = Vector3.Distance(node.Node.transform.position,
                            link.m_hitPlatform.transform.position) + node.DistanceToDestination;
                        
                        //find node in node list corresponding to the platform
                        var foundNode = finalNodeList.Where(p_node => p_node.Node.Equals(link.m_hitPlatform)).ToList();

                        //if no node was found that means the node does not exist break
                        if (!foundNode.Any())
                        {
                            Debug.Log("Node does not exist ");
                            break;
                        }

                        //if the platform distance is lower then update the distance of the linked node and its parent to the current node we are visiting
                        if (distance < foundNode[0].DistanceToDestination)
                        {
                            foundNode[0].UpdateDistance(distance);
                            foundNode[0].UpdateParent(node.Node);
                        }

                        //checks to see if this node is present in the unvisited node list
                        var unvisitedNode = nodestoVisit.Where(p_node => p_node.Node.Equals(link.m_hitPlatform)).ToList();
                        
                        //if no node is present then do not add it to the queue
                        if (!unvisitedNode.Any())
                        {
                            Debug.Log("Node already visited not adding it to queue");
                        }
                        else
                        {
                            adjacentPlatformQueue.Enqueue(unvisitedNode[0]);
                            nodestoVisit.Remove(unvisitedNode[0]);
                        }
                    }

                    node = adjacentPlatformQueue.Dequeue();
                }
                
                m_paths.Add(finalNodeList);
            }
        }
        
        private List<PlatformPath> GetPathingList()
        {
            var pathingList = new List<PlatformPath>();
            var platformGround = GameObject.FindGameObjectWithTag("Ground").GetComponent<Platform>();
            
            pathingList.AddRange(m_activePlatforms.Select(p_platform => new PlatformPath(p_platform, null, Mathf.Infinity)));
            pathingList.Add(new PlatformPath(GameObject.FindGameObjectWithTag("Ground").GetComponent<Platform>(), null, Mathf.Infinity));
            
            return pathingList;
        }

        #endregion
    }
}