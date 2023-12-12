using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory Inventory;
    public GameObject _visibleItem;
    [SerializeField] private float _health = 100;
    private bool _toTake;
                                    
    public float Health => _health;

    private void Awake()
    {
        Inventory = FindObjectOfType<Inventory>();
    }
    private void OnTake()
    {
        if (!_toTake && !_visibleItem) return;
        Take();
    }
    private void Take()
    {
        for (int j = 0; j < Inventory.Slots.Length; j++)
        {
            if (Inventory.IsFull[j] == false)
            {
                GameObject ObjectPrefab = _visibleItem.GetComponent<Item>().ObjectPrefab;
                Inventory.Slots[j] = ObjectPrefab;
                Inventory.IsFull[j] = true;
                Inventory.Icons[j] = ObjectPrefab.GetComponent<SpriteRenderer>().sprite;
                Destroy(_visibleItem);
                break;
            }
            else
            {
                print("места нету");
            }

        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Item" && _toTake == false)
        {
            _toTake = true;
            _visibleItem = collision.gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Item" && _toTake == true)
        {
            _toTake = false;
            _visibleItem = null;

        }
    }
}
