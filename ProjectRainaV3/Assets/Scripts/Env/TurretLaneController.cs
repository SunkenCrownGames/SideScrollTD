﻿using System;
using System.Collections.Generic;
using Player.UI;
using UnityEngine;
using UnityEngine.Rendering.Experimental.LookDev;

namespace Env
{
    public class TurretLaneController : MonoBehaviour
    {
        [SerializeField] private BoxType m_boxType;
        [SerializeField] private List<TurretSpawnController> m_spawnControllers;

        private void Awake()
        {
            
        }

        private void Start()
        {
            Setup();
        }

        private void Setup()
        {
            foreach (var spawnController in m_spawnControllers)
            {
                if (m_boxType == BoxType.Fade)
                {
                    spawnController.SetupFade();
                }
                else
                {
                    spawnController.TriggerBoxOff();
                }
            }
        }

        private void OnMouseEnter()
        {
            TriggerBoxOn();
        }

        private void OnMouseExit()
        {
            TriggerBoxOff();
        }

        private void TriggerBoxOn()
        {

            if (ResultSlotController.Instance.MergedData == null) return;
            
            foreach (var controller in m_spawnControllers)
            {
                controller.TriggerBox();
            }
        }

        private void TriggerBoxOff()
        {
            if (ResultSlotController.Instance.MergedData == null) return;
            
            foreach (var controller in m_spawnControllers)
            {
                controller.TriggerBoxOff();
            }
        }
    }
}
