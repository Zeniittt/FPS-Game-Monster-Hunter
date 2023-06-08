using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using SimpleJSON;
using UnityEditor.Rendering.LookDev;

public class ItemRow : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _name, _amount, _price, _quantity;
    [SerializeField]
    public Image thisRenderer;
    [SerializeField]
    public string _url;

    //Attribute
    public string itemName, amount, price, url;
    public int id;

    public double _total;

    ShopLoad _shopLoad;

    InventoryLoad _inventoryLoad;

    #region logic for shoping

    private void Start()
    {
        _shopLoad = FindObjectOfType<ShopLoad>();

        if (_shopLoad == null)
        {
            Debug.LogError("The Shop Load is NULL.");
        }
    }
    public void AssignValue()
    {
        _name.text = itemName;
        _amount.text = amount;
        _price.text = "$" + price;
        _url = url;
        StartCoroutine(LoadImageFromUrl());
    }

    private IEnumerator LoadImageFromUrl()
    {
        WWW wwwLoader = new WWW(_url);
        yield return wwwLoader;
        thisRenderer.sprite = Sprite.Create(wwwLoader.texture, new Rect(0, 0, wwwLoader.texture.width, wwwLoader.texture.height), Vector2.one / 2);
    }

    public void Add()
    {
        if (_quantity.text == _amount.text)
        {
            return;
        }
        _quantity.text = (Int32.Parse(_quantity.text) + 1).ToString();
        _total = Int32.Parse(price);
        _shopLoad.UpdateTotalMoney(_total);
    }

    public void Minus()
    {
        if (_quantity.text == "0")
        {
            return;
        }
        _quantity.text = (Int32.Parse(_quantity.text) - 1).ToString();
        _total = -Int32.Parse(price);
        _shopLoad.UpdateTotalMoney(_total);
    }

    public void ReduceAmountAfterBuy()
    {
        if (Int32.Parse(_quantity.text) != 0)
        {
            int amount = (Int32.Parse(_amount.text) - Int32.Parse(_quantity.text));
            _amount.text = amount.ToString();
            _quantity.text = "0";
            StartCoroutine(UpdateItem(id, amount));
        }

    }

    IEnumerator UpdateItem(int itemId, int amount)
    {
        WWWForm updateItemsForm = new WWWForm();
        updateItemsForm.AddField("id", itemId);
        updateItemsForm.AddField("amount", amount);
        UnityWebRequest updateItemsRequest = UnityWebRequest.Post("http://localhost/shop/updateAmount.php", updateItemsForm);
        yield return updateItemsRequest.SendWebRequest();
        if (updateItemsRequest.error == null)
        {
            Debug.Log("Updated!");
            Debug.Log(updateItemsRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log(updateItemsRequest.error);
        }
    }

    #endregion

    #region Logic for inventory

    public void InsertItemToInventory(int itemId, string itemName, int price, string url)
    {
        int amount = Int32.Parse(_quantity.text);
        if (Int32.Parse(_quantity.text) != 0)
        {
            StartCoroutine(InsertItem(itemId,itemName,price,amount,url));
        }
    }

    IEnumerator InsertItem(int itemId, string itemName, int price , int amount, string url)
    {
        WWWForm InsertItemsForm = new WWWForm();
        InsertItemsForm.AddField("id", itemId);
        InsertItemsForm.AddField("name", itemName);
        InsertItemsForm.AddField("price", price);
        InsertItemsForm.AddField("amount", amount);
        InsertItemsForm.AddField("url", url);
        UnityWebRequest InsertItemsRequest = UnityWebRequest.Post("http://localhost/inventory/insertItem.php", InsertItemsForm);
        yield return InsertItemsRequest.SendWebRequest();
        if (InsertItemsRequest.error == null)
        {
            Debug.Log("Inserted!");
            Debug.Log(InsertItemsRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log(InsertItemsRequest.error);
        }
    }

    #endregion
}
