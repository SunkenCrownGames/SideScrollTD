using System.Collections;
using System.Collections.Generic;
using AngieTools.V2Tools;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        var groundPlane = new Plane(Vector3.up, Vector3.zero);
        var rayLength = 0.0f;


        if (groundPlane.Raycast(camRay, out rayLength))
        {
            Vector3 pointToLook = camRay.GetPoint(rayLength);
            Debug.DrawLine(camRay.origin, pointToLook, Color.blue);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
}
