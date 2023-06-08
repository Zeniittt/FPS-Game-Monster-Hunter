using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class InventoryLoad : MonoBehaviour
{
    
    public GameObject _content;
    public GameObject _itemPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetAllItems());
    }

    public void UpdateInventory()
    {
        foreach(Transform item in _content.transform)
        {
            Destroy(item.gameObject);
        }
        StartCoroutine(GetAllItems());
    }

    IEnumerator GetAllItems()
    {
        WWWForm getAllItemsForm = new WWWForm();
        UnityWebRequest getAllItemsRequest = UnityWebRequest.Post("http://localhost/inventory/getallInventory.php", getAllItemsForm);
        yield return getAllItemsRequest.SendWebRequest();
        if (getAllItemsRequest.error == null)
        {
            JSONNode allItems = JSON.Parse(getAllItemsRequest.downloadHandler.text);
            Debug.Log(getAllItemsRequest.downloadHandler.text);
            foreach (JSONNode item in allItems)
            {
                var inventoryItem = Instantiate(_itemPrefab, new Vector3(), Quaternion.identity);
                inventoryItem.transform.SetParent(_content.transform);
                inventoryItem.GetComponent<InventorySlot>().id = item[0];
                inventoryItem.GetComponent<InventorySlot>().itemName = item[1];
                inventoryItem.GetComponent<InventorySlot>().price = item[2];
                inventoryItem.GetComponent<InventorySlot>().amount = item[3];
                inventoryItem.GetComponent<InventorySlot>().url = item[4];
                inventoryItem.GetComponent<InventorySlot>().AssignValue();
            }
        }
        else
        {
            Debug.Log(getAllItemsRequest.error);
        }
    }

}
