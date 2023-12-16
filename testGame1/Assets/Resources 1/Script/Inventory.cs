using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject[] SlotsImages;
    public Sprite[] Icons;
    public GameObject SelectWrapper;
    public bool[] IsFull;
    public GameObject[] Slots;
    public int SlotsMenegerInt = 0;
}
