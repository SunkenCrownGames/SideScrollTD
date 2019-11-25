using System;
using Player.Selection;
using UnityEngine;

namespace Env
{
    public class TurretSpawnController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnMouseEnter()
        {
            Debug.Log("Mouse entered");

            if (SelectionPrefabsController.Instance.ActiveSelection == null) return;
            
            SelectionPrefabsController.Instance.ActiveSelection.LinkedObject.gameObject.transform.position =
                transform.position;
            SelectionPrefabsController.Instance.ActiveSelection.LinkedObject.gameObject.SetActive(true);
        }

        private void OnMouseExit()
        {
            if (SelectionPrefabsController.Instance.ActiveSelection == null) return;
            
            Debug.Log("Mouse exited");
            SelectionPrefabsController.Instance.ActiveSelection.LinkedObject.gameObject.SetActive(false);
        }
    }
}
