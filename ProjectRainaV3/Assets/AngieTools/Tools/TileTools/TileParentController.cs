using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngieTools.Tiles
{
    public class TileParentController : MonoBehaviour
    {
        private static TileParentController m_instance;

        [SerializeField]
        private List<GameObject> m_tilesList = null;
        [SerializeField]
        private bool m_onTiles;


        private void Awake()
        {
            BindInstance();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void BindInstance()
        {
            if (m_instance == null)
            {
                m_instance = this;
            }
            else
            {
                Debug.LogError("Instance Already Bound Please Check: " + gameObject.name + " For Duplicate");
            }
        }

        public void CheckOnUIOnAllTiles()
        {
            foreach (GameObject tile in m_tilesList)
            {
                BaseTileController btc = tile.GetComponent<BaseTileController>();

                if (btc != null)
                {
                    if (btc.OnTile)
                    {
                        m_onTiles = true;
                        break;
                    }
                }

                m_onTiles = false;
            }
        }
        

        public static TileParentController Controller
        {
            get { return m_instance; }
        }

        public List<GameObject> TileList
        {
            get { return m_tilesList; }
        }

        public bool OnTiles
        {
            get { return m_onTiles; }
        }
    }
}
