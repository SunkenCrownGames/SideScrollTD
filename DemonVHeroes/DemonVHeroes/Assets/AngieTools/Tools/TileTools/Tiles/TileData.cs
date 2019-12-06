using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngieTools.Tiles
{
    [System.Serializable]
    public class BaseTileData
    {
        [SerializeField]
        protected BaseTileController m_topTile = null;
        [SerializeField]
        protected BaseTileController m_bottomTile = null;
        [SerializeField]
        protected BaseTileController m_leftTile = null;
        [SerializeField]
        protected BaseTileController m_rightTile = null;

        [SerializeField]
        protected int m_columnID = 0;
        [SerializeField]
        protected int m_rowID = 0;


        [SerializeField]
        protected GameObject m_activeEntity = null;


        public BaseTileData()
        {
            m_topTile = null;
            m_bottomTile = null;
            m_leftTile = null;
            m_rightTile = null;
            m_activeEntity = null;
        }


        public BaseTileController TopTile
        {
            get { return m_topTile; }
            set { m_topTile = value; }
        }

        public BaseTileController BottomTile
        {
            get { return m_bottomTile; }
            set { m_bottomTile = value; }
        }

        public BaseTileController LeftTile
        {
            get { return m_leftTile; }
            set { m_leftTile = value; }
        }

        public BaseTileController RightTile
        {
            get { return m_rightTile; }
            set { m_rightTile = value; }
        }

        public int RowID
        {
            get { return m_rowID; }
            set { m_rowID = value; }
        }

        public int ColumnID
        {
            get { return m_columnID; }
            set { m_columnID = value; }
        }

        public GameObject Entity
        {
            get { return m_activeEntity; }
            set { m_activeEntity = value; }
        }
    }
}