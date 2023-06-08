using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;

public class ShopLoad : MonoBehaviour
{
    public GameObject _itemPrefab;
    public GameObject _content;
    private double _totalPrice = 0;
    public UIManager _uIManager;

    [SerializeField]
    private TMP_Text _purseText;

    [SerializeField]
    private double _purse = 5000000f;

    public GameObject _notification;

    [SerializeField]
    private InventoryLoad _inventoryLoad;


    private static List<ItemRow> _itemList = new List<ItemRow>();

    // Start is called before the first frame update
    void Start()
    {
        _uIManager = GameObject.FindObjectOfType<UIManager>();
        _inventoryLoad = GameObject.FindObjectOfType<InventoryLoad>();
        if (PlayerPrefs.GetString("purse") != null)
        {
            _purseText.text = "$" + PlayerPrefs.GetString("purse");
            _purse = double.Parse(PlayerPrefs.GetString("purse"));
        }
        else
        {
            _purse = 5000000f;
            PlayerPrefs.SetString("purse", _purse.ToString());
        }

        StartCoroutine(GetAllItems());
    }

    //get all item
    IEnumerator GetAllItems()
    {
        WWWForm getAllItemsForm = new WWWForm();
        UnityWebRequest getAllItemsRequest = UnityWebRequest.Post("http://localhost/shop/getall.php", getAllItemsForm);
        yield return getAllItemsRequest.SendWebRequest();
        if (getAllItemsRequest.error == null)
        {
            JSONNode allItems = JSON.Parse(getAllItemsRequest.downloadHandler.text);
            foreach (JSONNode item in allItems)
            {
                var shopItem = Instantiate(_itemPrefab, new Vector3(), Quaternion.identity);
                shopItem.transform.SetParent(_content.transform);
                shopItem.GetComponent<ItemRow>().id = item[0];
                shopItem.GetComponent<ItemRow>().itemName = item[1];
                shopItem.GetComponent<ItemRow>().price = item[2];
                shopItem.GetComponent<ItemRow>().amount = item[3];
                shopItem.GetComponent<ItemRow>().url = item[4];
                shopItem.GetComponent<ItemRow>().AssignValue();
                _itemList.Add(shopItem.GetComponent<ItemRow>());
            }
        }
        else
        {
            Debug.Log(getAllItemsRequest.error);
        }
    }

    public void UpdateTotalMoney(double total)
    {
        _totalPrice += total;
        _uIManager.UpdateTotalMoney(_totalPrice);
    }

    public void ClickBuy()
    {
        if (_purse >= _totalPrice)
        {
            //
            foreach (ItemRow item in _itemList)
            {
                item.InsertItemToInventory(item.id, item.itemName, int.Parse(item.price), item.url);
                item.ReduceAmountAfterBuy();
            }
            _inventoryLoad.UpdateInventory();
            _purse -= _totalPrice;
            _totalPrice = 0;
            _uIManager.UpdateTotalMoney(_totalPrice);
            _purseText.text = "$" + _purse;
            PlayerPrefs.SetString("purse", _purse.ToString());
        }
        else
        {
            StartCoroutine(StartNotification());
        }
    }

    IEnumerator StartNotification()
    {
        _notification.SetActive(true);
        yield return new WaitForSeconds(1f);
        _notification.SetActive(false);
    }

}
