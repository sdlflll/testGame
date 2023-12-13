using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public InputAction PlayerMainControls;
    public Inventory Inventory;
    public GameObject _visibleItem;
    [SerializeField] private float _health = 100;
    private bool _toTake;
                                    
    public float Health => _health;

    private void Awake()
    {
        Inventory = FindObjectOfType<Inventory>();
    }
    private void FixedUpdate()
    {
        HealthHandler();
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
                Inventory.SlotsImages[j].GetComponent<Image>().enabled = true;
                Inventory.SlotsImages[j].GetComponent<Image>().sprite = Inventory.Icons[j];
                Destroy(_visibleItem);
                break;
            }
            else
            {
                print("места нету");
            }

        }
    }

    private void HealthHandler()
    {
        if(Health <= 0)
        {
            Destroy(gameObject);
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
