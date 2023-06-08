using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeDecorator : DecoratorWeapon
{
    private AWeapon inner;

    [SerializeField] Camera fpsCamera;
    [SerializeField] float zoomOutFOV = 60f, zoomInFOV = 20f;

    bool zoomInToggle = false;

    public ScopeDecorator(AWeapon inner, Camera camera) : base(inner)
    {
        fpsCamera = camera;
    }

    private void Start()
    {
        zoomOutFOV = 60f; zoomInFOV = 20f;
        FindObjectOfType<Camera>().fieldOfView = zoomOutFOV;

    }

    public void Initialize(AWeapon inner, Camera camera)
    {
        this.inner = inner;
        fpsCamera = camera;
    }

    private void Awake()
    {
        zoomOutFOV = 60f; zoomInFOV = 20f;
        FindObjectOfType<Camera>().fieldOfView = zoomOutFOV;
        fpsCamera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        Aim();
    }

    public override void Shoot()
    {
        Aim();
        base.Shoot();
    }

    private void Aim()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!zoomInToggle)
            {
                zoomInToggle = true;
                fpsCamera.fieldOfView = zoomInFOV;
            }
            else
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
