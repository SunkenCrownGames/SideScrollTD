using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AngieTools.V2Tools.Pathing.Dijkstra
{
    /*public class PathingManager : MonoBehaviour
    {
        private List<List<PlatformPath>> m_paths;
        private List<Platform> m_activePlatforms;

        [SerializeField] private Transform m_spawnersParent;

        private void Awake()
        {
            BindInstance();
        }

        private void BindInstance()
        {
            if(Instance != null) Destroy(gameObject);

            Instance = this;
        }

        public void UpdatePaths(List<List<PlatformPath>> p_paths, List<Platform> p_activePlatforms)
        {
            m_paths = p_paths;
            m_activePlatforms = p_activePlatforms;
        }
        
        public static Stack<Platform> GetPath(Platform p_startPlatform, Platform p_destinationPlatform)
        {
            if (Instance == null) return null;


            var platformPath = new Stack<Platform>();
            List<PlatformPath> finalPath = null;
            
            foreach (var path in Instance.m_paths)
            {
                // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
                foreach (var pathNode in path)
                {
                    if (pathNode.Node != p_startPlatform || pathNode.ParentNode != null) continue;
                    
                    Debug.Log($"Found Start Node: {pathNode.Node.name}");
                    
                    finalPath = path;
                    break;
                }
            }

            platformPath.Push(p_destinationPlatform);
            if (finalPath != null)
            {
                var endNode = finalPath.Where(p_path => p_path.Node == p_destinationPlatform).ToList()[0];

                while (endNode.ParentPlatformPathNode != null)
                {
                    platformPath.Push(endNode.ParentNode);
                    endNode = endNode.ParentPlatformPathNode;
                }
                
                
                return platformPath;
            }
            else
            {
                return null;
            }
        }

        public static Platform RandomPlatform()
        {
            var platformPosition = Random.Range(0, Instance.m_activePlatforms.Count);

            return Instance.m_activePlatforms[platformPosition];

        }

        public List<List<PlatformPath>> Paths => m_paths;

        public List<Platform> ActivePlatforms => m_activePlatforms;

        public Transform SpawnersParent => m_spawnersParent;

        public static PathingManager Instance { get; private set; }
    }*/
}
