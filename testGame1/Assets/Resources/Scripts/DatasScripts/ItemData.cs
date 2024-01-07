using UnityEngine;


[CreateAssetMenu(menuName = "StatsData/ItemData", fileName = "ItemData")]
public class ItemData : ScriptableObject
{
    public string itemType;
    public string itemName;
    public bool canTake;
    public GameObject objectPrefab;
}
