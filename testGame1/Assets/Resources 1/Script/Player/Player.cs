using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageble
{
    [SerializeField] private ParticleSystem _getDamagePS;

    public InputAction playerMainControls;
    public Inventory Inventory;
    public GameObject visibleItem;
    private float _playerHealth = 100;
    private bool _toTake;

                                    
    public float Health => _playerHealth;

    private void Awake()
    {
        
        Inventory = FindObjectOfType<Inventory>();
    }
    private void FixedUpdate()
    {
        HealthHandler();
    }

    public void GetDamage(float Damage)
    {
        _getDamagePS.Play();
        _playerHealth -= Damage;
    }

    private void OnInventoryMeneger()
    {
        Inventory.SlotsMenegerInt++;
        if(Inventory.SlotsMenegerInt > 2)
        {
            Inventory.SlotsMenegerInt = 0;
        }
        switch(Inventory.SlotsMenegerInt)
        {
            case 0:
                Inventory.SelectWrapper.transform.DOLocalMoveY(65, 0.3f);
                break;
            case 1:
                Inventory.SelectWrapper.transform.DOLocalMoveY(-0, 0.3f);
                break;
            case 2:
                Inventory.SelectWrapper.transform.DOLocalMoveY(-65, 0.3f);
                break;
        }
    }

    private void OnTake()
    {
        if (!_toTake && !visibleItem) return;
        for (int j = 0; j < Inventory.Slots.Length; j++)
        {
            if (Inventory.IsFull[j] == false)
            {
                GameObject ObjectPrefab = visibleItem.GetComponent<Item>().objectPrefab;
                Inventory.Slots[j] = ObjectPrefab;
                Inventory.IsFull[j] = true;
                Inventory.Icons[j] = ObjectPrefab.GetComponent<SpriteRenderer>().sprite;
                Inventory.SlotsImages[j].GetComponent<Image>().enabled = true;
                Inventory.SlotsImages[j].GetComponent<Image>().sprite = Inventory.Icons[j];
                Destroy(visibleItem);
                break;
            }
            else
            {
                print("места нету");
            }

        }
    }

    private void OnDropItem()
    {
        if (Inventory.IsFull[Inventory.SlotsMenegerInt] == true)
        {
            Instantiate(Inventory.Slots[Inventory.SlotsMenegerInt].GetComponent<Item>().objectPrefab, gameObject.transform.position, new Quaternion(0,0,0,0));
            Inventory.Slots[Inventory.SlotsMenegerInt] = null;
            Inventory.Icons[Inventory.SlotsMenegerInt] = null;
            Inventory.IsFull[Inventory.SlotsMenegerInt] = false;
            Inventory.SlotsImages[Inventory.SlotsMenegerInt].GetComponent<Image>().sprite = null;
            Inventory.SlotsImages[Inventory.SlotsMenegerInt].GetComponent<Image>().enabled = false;
        }
    }

    private void HealthHandler()
    {
        if(Health <= 0)
        {
            Destroy(gameObject);
        }
    }





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item" && _toTake == false)
        {
            _toTake = true;
            visibleItem = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item" && _toTake == true)
        {
            _toTake = false;
            visibleItem = null;
        }
    }

}
