using System;
using System.Collections.Generic;
using AngieTools.Effects.Fade;
using Player.UI;
using UnityEngine;
using UnityEngine.Rendering.Experimental.LookDev;

namespace Env
{
    public class TurretLaneController : MonoBehaviour
    {
        [SerializeField] private List<TurretSpawnController> m_spawnControllers = null;

        private void Awake()
        {
            
        }

        private void Start()
        {
            
        }

        public void ToggleSpawnControllers(bool p_toggle)
        {
            foreach (var turretSpawnController in m_spawnControllers)
            {
                turretSpawnController.SetState(p_toggle);
            }
        }
    }
}
