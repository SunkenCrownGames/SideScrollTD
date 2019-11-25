using Player.MergedTurret.Data;
using UnityEngine;

namespace Player.Selection
{
    public class SelectionData
    {
        public SelectionData(GameObject p_linkedObject, MergedData p_data)
        {
            LinkedObject = p_linkedObject;
            Data = p_data;
        }

        public GameObject LinkedObject { get; set; }
        public MergedData Data { get; set; }
        
        public bool Status { get; set; }
    }
}
