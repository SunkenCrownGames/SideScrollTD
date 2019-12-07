using System.Collections.Generic;
using System.Linq;
using AngieTools;
using AngieTools.Tools.DataStructure;
using Sirenix.OdinInspector;
using UnityEngine;
using System;
using UnityScript.Lang;
using Array = System.Array;

namespace Level
{
    public class Platform : MonoBehaviour
    {
        [Title("Links")] 
        [SerializeField] private List<Ladder> m_ladders;
        [SerializeField] private HitPlatformResult m_topLink;
        [ShowInInspector] [SerializeField] private HitPlatformResult m_bottomLink;

        [Title("Data")] 
        [SerializeField] private int m_linkCount = 0;

        [SerializeField] private List<PlatformPath> m_paths;

        // Start is called before the first frame update
        void Start()
        {
        
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

        private void OnMouseDown()
        {
            
        }

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

                    if (result) m_linkCount++;
                    
                    break;
                }
            }
        }
        
        public void BreakLadder()
        {
            var linkToBreak = UnityEngine.Random.Range(0, m_bottomLink.Data.Count);
            m_bottomLink.Data[linkToBreak].m_hitPlatform.TopLink.Data.Remove(m_bottomLink.Data[linkToBreak]);
            m_bottomLink.Data.RemoveAt(linkToBreak);
            m_linkCount--;
        }

        public List<Ladder> Ladders => m_ladders;

        public HitPlatformResult TopLink => m_topLink;

        public HitPlatformResult BottomLink => m_bottomLink;

        public int LinkCount => m_linkCount;
    }
}
