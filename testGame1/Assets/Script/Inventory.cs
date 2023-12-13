using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject[] SlotsImages;
    public Sprite[] Icons;
    public bool[] IsFull;
    public GameObject[] Slots;
    private int _slotsMeneger = 0;
    private Player Player;
    private InputAction PlayerMainControls;

    private void Start()
    {
        Player = FindObjectOfType<Player>();
        PlayerMainControls = Player.PlayerMainControls;
    }

private void OnInventoryMeneger(InputValue value)
    {
        switch (_slotsMeneger)
        {
            case 1:
                print("גפûג");
                break;
        }

    }
}
