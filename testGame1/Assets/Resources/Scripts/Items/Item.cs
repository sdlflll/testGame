using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;
    public string itemType;
    public bool canTake;
    public string itemName;
    public GameObject objectPrefab;
    private void Awake()
    {
        itemType = itemData.itemType;
        canTake = itemData.canTake;
        itemName = itemData.itemName;
        objectPrefab = itemData.objectPrefab;
    }

}
