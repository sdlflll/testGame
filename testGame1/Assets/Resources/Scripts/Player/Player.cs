using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageble
{
    public UnityEvent onDamagePlayerEvent = new UnityEvent();

    private Inventory _inventory;
    private Item _visibleItem;
    private float _playerHealth = 100;
    private bool _toTake;
    private HealthBar _healthBar;
    private PlayableDirector _deathTimeline;

    public float Health => _playerHealth;

    private void Awake()
    {
        _deathTimeline = GetComponent<PlayableDirector>();
        _inventory = FindObjectOfType<Inventory>();
        _healthBar = FindObjectOfType<HealthBar>();
    }

    public void GetDamage(float Damage)
    {
        onDamagePlayerEvent.Invoke();
        _playerHealth -= Damage;
        HealthHandler();
        _healthBar.HealthBarHandler();
    }

    private void OnInventoryMeneger()
    {
        _inventory.SlotsMenegerInt++;
        if(_inventory.SlotsMenegerInt > 2)
        {
            _inventory.SlotsMenegerInt = 0;
        }
        switch(_inventory.SlotsMenegerInt)
        {
            case 0:
                _inventory.SelectWrapper.transform.DOLocalMoveY(65, 0.3f);
                break;
            case 1:
                _inventory.SelectWrapper.transform.DOLocalMoveY(-0, 0.3f);
                break;
            case 2:
                _inventory.SelectWrapper.transform.DOLocalMoveY(-65, 0.3f);
                break;
        }
    }

    private void OnTake()
    {
        if (!_toTake && !_visibleItem) return;

        for (int j = 0; j < _inventory.Slots.Length; j++)
        {
            if (_inventory.IsFull[j] == false)
            {
                GameObject ObjectPrefab = _visibleItem.objectPrefab;
                _inventory.Slots[j] = ObjectPrefab;
                _inventory.IsFull[j] = true;
                _inventory.Icons[j] = ObjectPrefab.GetComponent<SpriteRenderer>().sprite;
                _inventory.SlotsImages[j].GetComponent<Image>().enabled = true;
                _inventory.SlotsImages[j].GetComponent<Image>().sprite = _inventory.Icons[j];
                Destroy(_visibleItem.gameObject);
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
        if (_inventory.IsFull[_inventory.SlotsMenegerInt] == true)
        {
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 13baa4e7b3f4cb7d21b49c23356d1164f78bf7f9
            Instantiate(_inventory.Slots[_inventory.SlotsMenegerInt].GetComponent<Item>().objectPrefab, gameObject.transform.position, new Quaternion(0,0,0,0));
            _inventory.Slots[_inventory.SlotsMenegerInt] = null;
            _inventory.Icons[_inventory.SlotsMenegerInt] = null;
            _inventory.IsFull[_inventory.SlotsMenegerInt] = false;
            _inventory.SlotsImages[_inventory.SlotsMenegerInt].GetComponent<Image>().sprite = null;
            _inventory.SlotsImages[_inventory.SlotsMenegerInt].GetComponent<Image>().enabled = false;
<<<<<<< HEAD
=======
=======
            Instantiate(Inventory.Slots[Inventory.SlotsMenegerInt], gameObject.transform.position, new Quaternion(0,0,0,0));
            Inventory.Slots[Inventory.SlotsMenegerInt] = null;
            Inventory.Icons[Inventory.SlotsMenegerInt] = null;
            Inventory.IsFull[Inventory.SlotsMenegerInt] = false;
            Inventory.SlotsImages[Inventory.SlotsMenegerInt].GetComponent<Image>().sprite = null;
            Inventory.SlotsImages[Inventory.SlotsMenegerInt].GetComponent<Image>().enabled = false;
>>>>>>> origin/main
>>>>>>> 13baa4e7b3f4cb7d21b49c23356d1164f78bf7f9
        }
    }

    private void HealthHandler()
    {
        if(Health <= 0)
        {
            _playerHealth = 0;
            _deathTimeline.Play();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
    public void UnplugPlayerMove()
    {
        CinemachineVirtualCamera Camera = FindObjectOfType<CinemachineVirtualCamera>();
        PlayerMove PlayerMove = GetComponent<PlayerMove>();
        PlayerMove.rb.bodyType = RigidbodyType2D.Static;
        PlayerMove.enabled = false;
        Camera.Follow = gameObject.transform;
        Camera.LookAt = gameObject.transform;
        Camera.m_Lens.OrthographicSize = 1.5f;

    }





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item" && _toTake == false)
        {
            _toTake = true;
            _visibleItem = collision.gameObject.GetComponent<Item>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item" && _toTake == true)
        {
            _toTake = false;
            _visibleItem = null;
        }
    }

}
