using System;
using AngieTools.GameObjectTools;
using UnityEngine;

namespace Player.Selection
{
    public class SelectionTurretController : MonoBehaviour
    {


        public void UpdatePosition(Vector3 p_pos)
        {
            transform.position = p_pos;
        }
    }
}
