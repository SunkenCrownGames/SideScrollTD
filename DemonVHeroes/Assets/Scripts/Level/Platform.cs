using System.Collections.Generic;
using System.Linq;
using AngieTools;
using AngieTools.Tools.DataStructure;
using Sirenix.OdinInspector;
using UnityEngine;
using System;
using AngieTools.V2Tools.Pathing.Dijkstra;
using Enemy;
using Spawners;
using Spawners.UI;
using Array = System.Array;

namespace Level
{
    public class Platform : MonoBehaviour
    {

        [Title("Spawners")]
        [SerializeField] private List<Spawner> m_spawnerZones;
        
        [Title("Links")] 
        [SerializeField] private List<Ladder> m_ladders;
        [SerializeField] private HitPlatformResult m_topLink;
        [ShowInInspector] [SerializeField] private HitPlatformResult m_bottomLink;

        [Title("Data")] 
        [SerializeField] private List<EnemyController> m_enemiesOnPlatform;
        [SerializeField] private SpriteRenderer m_sr;
        [SerializeField] private int m_linkCount = 0;

        [SerializeField] private List<PlatformPath> m_paths;

        
        private static Platform _startPlatform;
        [SerializeField] private Range m_platformWidth;

        // Start is called before the first frame update
        void Start()
        {
            GetWidth();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        [Button("Generate Link")]
        public void Link()
        {
            var hits = new RaycastHit2D[5];
            var hitPositions = LevelGenerator.GetRayXPositions(gameObject);
            LinkBottom(hits, hitPositions);
            Array.Clear(hits, 0, hits.Length);
            LinkTop(hits, hitPositions);
        }

        [Button("Update Start Platform")]
        public void UpdateStartPlatform()
        {
            _startPlatform = this;
        }
        
        [Button("Generate Path")]
        public void Path()
        {
            Debug.Log($"Start Platform {_startPlatform.name} End Platform: {this.name}");
            var path = PathingManager.GetPath(_startPlatform, this);

            while (path.Count > 0)
            {
                Debug.Log(path.Pop().name);
            }
        }

        private void GetWidth()
        {
            var box = m_sr.bounds;
            var position = transform.position;
            
            m_platformWidth.StartValue = position.x - box.extents.x;
            m_platformWidth.EndValue = position.x + box.extents.x;
        }

        #region Links
        
        private void LinkBottom(RaycastHit2D[] p_hits, Vector3[] p_hitPositions)
        {
            foreach (var position in p_hitPositions)
            {
                var hitCount = Physics2D.RaycastNonAlloc(position, Vector3.down, p_hits, 500, LayerMask.GetMask("Nodes"));
                
                if (hitCount <= 0) continue;
                
                Debug.DrawRay(position, Vector3.down * 500, Color.green, 50f);
                
                foreach(var hit in p_hits)
                {
                    if (hit.collider == null || hit.collider.gameObject == gameObject) continue;

                    if(hit.collider.CompareTag("Ground"))
                        hit.collider.GetComponent<Ground>().AddToPlatforms(this);
                    
                    var result = m_bottomLink.AddToResults(hit.collider.GetComponent<Platform>(), Direction.BOTTOM, position);
                        hit.collider.GetComponent<Platform>().TopLink.AddToResults(this, Direction.TOP, position);
                    if (result) m_linkCount++;
                    
                    break;
                }
            }
        }

        private void LinkTop(RaycastHit2D[] p_hits, Vector3[] p_hitPositions)
        {
            foreach (var position in p_hitPositions)
            {
                var hitCount = Physics2D.RaycastNonAlloc(position, Vector3.up, p_hits, 500, LayerMask.GetMask("Nodes"));
                
                if (hitCount <= 0) continue;
                
                Debug.DrawRay(position, Vector3.up * 500, Color.green, 50f);
                
                foreach(var hit in p_hits)
                {
                    if (hit.collider == null || hit.collider.gameObject == gameObject) continue;
                    
                    
                    var result = m_topLink.AddToResults(hit.collider.GetComponent<Platform>(), Direction.TOP, position);

                    break;
                }
            }
        }
        
        public void BreakLadder()
        {
            var linkToBreak = UnityEngine.Random.Range(0, m_bottomLink.Data.Count);
            var platform = m_bottomLink.Data[linkToBreak].m_hitPlatform;
            m_bottomLink.Data.RemoveAt(linkToBreak);
            m_linkCount--;

            for (var i = 0; i < platform.TopLink.Data.Count; i++)
            {
                if (!platform.TopLink.Data[i].m_hitPlatform.Equals(this)) continue;
                
                platform.TopLink.Data.RemoveAt(i);
                //Debug.Log("Found Problematic Platform;");
                break;
            }
        }

        #endregion
        public List<Ladder> Ladders => m_ladders;

        public HitPlatformResult TopLink => m_topLink;

        public HitPlatformResult BottomLink => m_bottomLink;

        public Range PlatformWidth => m_platformWidth;
        
        
        public SpriteRenderer SpriteRenderer => m_sr;

        public void UpdateSpawners()
        {
            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
            foreach (var spawnerZone in m_spawnerZones)
            {
                if (SpawnerUiController.Instance.SelectedSlot == null) break;
                
                spawnerZone.UpdateSpawnerVisual();
            }
        }

        public void AddEnemyToPlatform(EnemyController p_enemyController)
        {
            m_enemiesOnPlatform.Add(p_enemyController);
        }

        public void RemoveEnemyFromPlatform(EnemyController p_enemyController)
        {
            m_enemiesOnPlatform.Remove(p_enemyController);
        }
        
        public void DisableSpawners()
        {
            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
            foreach (var spawnerZone in m_spawnerZones)
            {
                if (SpawnerUiController.Instance.SelectedSlot == null) break;

                spawnerZone.DisableSpawnerVisual();
            }
        }


        public List<EnemyController> EnemiesOnPlatform => m_enemiesOnPlatform;

        public int LinkCount => m_linkCount;
        
        
    }
}
