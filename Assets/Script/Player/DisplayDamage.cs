using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDamage : MonoBehaviour
{
    [SerializeField] Canvas _impactCanvas;
    [SerializeField] float _impactTime = 0.3f;

    private void Start()
    {
        _impactCanvas.enabled = false;
    }

    public void ShowDamageImpact()
    {
        StartCoroutine(ShowSplatter());
    }

    IEnumerator ShowSplatter()
    {
        _impactCanvas.enabled = true;
        yield return new WaitForSeconds(_impactTime);
        _impactCanvas.enabled = false;
    }
}
