using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : AWeapon
{
    [SerializeField] private float _range = 500f, _damage = 30f;
    [SerializeField] Camera _FPcamera;
    [SerializeField] ParticleSystem _muzzleFlash;
    [SerializeField] GameObject _hitEffect;
    [SerializeField] AmmoType _ammoType = null;
    [SerializeField] SniperAmmo _sniperAmmo = null;
    [SerializeField] ShotgunAmmo _shotgunAmmo = null;
    [SerializeField] TommyAmmo _tommyAmmo = null;
    [SerializeField] float _Cooldown = 0.5f;
    [SerializeField] bool _canShoot = true;
    [SerializeField] public TextMeshProUGUI _ammoText;

    private void Start()
    {
        _sniperAmmo = GetComponent<SniperAmmo>(); // Hoặc bất kỳ cách nào khác để lấy đối tượng SniperAmmo
        _shotgunAmmo = GetComponent<ShotgunAmmo>();
        _tommyAmmo = GetComponent<TommyAmmo>();
    }

    private void OnEnable()
    {
        _canShoot = true;
    }

    void Update()
    {
        DisplayAmmo();
        if (Input.GetMouseButtonDown(0) || _canShoot)
        {
            Shoot();
        }
    }

    public void DisplayAmmo()
    {
        string weaponName = GetComponent<Transform>().name;
        switch (weaponName)
        {
            case "Carbine":
                //_ammoText.SetText(_sniperAmmo.GetCurrentAmmo().ToString());
                break;
            case "Shotgun":
                //_ammoText.SetText(_shotgunAmmo.GetCurrentAmmo().ToString());
                break;
            case "Piston":
                //_ammoText.SetText(_tommyAmmo.GetCurrentAmmo().ToString());
                break;
            default:
                break;
        }
    }

    /*IEnumerator Shoot()
    {
        
    }*/

    private void PlayMuzzleFlash()
    {
        _muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        //Cách 1: referrence tới 1 camera
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, out hitInfo, _range))
        {
            Debug.Log("I hit: " + hitInfo.transform.name);
            CreatHitImpact(hitInfo);
            EnemyHealth target = hitInfo.transform.GetComponent<EnemyHealth>();
            if (target != null)
            {
                target.TakeDamage(_damage);
            }
        }

        //Cách 2: referrence tới nhiều camera
        /*RaycastHit hitInfo;
        Physics.Raycast(FPcamera.transform.position, FPcamera.transform.forward, out hitInfo, _range);
        Debug.Log("I hit: " + hitInfo.transform.name);*/
    }

    private void CreatHitImpact(RaycastHit hitInfo)
    {
        GameObject impact = Instantiate(_hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
        Destroy(impact, .1f);
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }

    public override void Shoot()
    {
        _canShoot = false;
        string weaponName = GetComponent<Transform>().name;
        switch (weaponName)
        {
            case "Carbine":
                _ammoType = GetComponent<SniperAmmo>();
                break;
            case "Shotgun":
                _ammoType = GetComponent<ShotgunAmmo>();
                break;
            case "Piston":
                _ammoType = GetComponent<TommyAmmo>();
                break;
        }

        /*if (_ammoType.GetCurrentAmmo() > 0)
        {
            *//*_ammoType.ReduceCurrentAmmo();*//*
        }*/
        ProcessRaycast();
        PlayMuzzleFlash();

        StartCoroutine(ResetShootCooldown());
    }

    private IEnumerator ResetShootCooldown()
    {
        yield return new WaitForSeconds(_Cooldown);
        _canShoot = true;
    }

}