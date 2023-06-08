using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [SerializeField] float zoomOutFOV = 60f, zoomInFOV = 20f;

    bool zoomInToggle = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!zoomInToggle)
            {
                zoomInToggle = true;
                fpsCamera.fieldOfView = zoomInFOV;
            }else
            {
                zoomInToggle = false;
                fpsCamera.fieldOfView = zoomOutFOV;
            }
        }
    }

    private void OnDisable()
    {
        zoomInToggle = false;
        fpsCamera.fieldOfView = zoomOutFOV;
    }

}
