using System;
using AngieTools.GameObjectTools;
using Player.Turrets;
using Spine.Unity;
using UnityEngine;

namespace Player.Selection
{
    public class SelectionTurretController : MonoBehaviour
    {
        private void Start()
        {
            
        }

        public void UpdatePosition(Vector3 p_pos)
        {
            transform.position = p_pos;
        }
        
    }
}
