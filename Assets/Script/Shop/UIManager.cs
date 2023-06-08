using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _total;

    [SerializeField]
    private GameObject _content;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _total.text = "Total: $" + 0;
    }


    public void UpdateTotalMoney(double total)
    {
        _total.text = "Total: $" + total.ToString();
    }

    public void OpenInventory()
    {
        _animator.SetTrigger("OpenInventory");
    }


}
