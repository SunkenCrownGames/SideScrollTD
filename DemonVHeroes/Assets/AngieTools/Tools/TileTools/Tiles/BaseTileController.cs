using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AngieTools.Tiles
{
    public class BaseTileController : MonoBehaviour
    {
        [SerializeField]
        protected BaseTileData m_data;
        protected bool m_onTile;

        private void Awake()
        {

        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            //TileParentController.Controller.TileList.Add(gameObject);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public BaseTileData Data
        {
            get { return m_data; }
            set { m_data = value; }
        }

        public bool OnTile
        {
            get { return m_onTile; }
            set { m_onTile = value; }
        }
    }
}