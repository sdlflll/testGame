using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    /*[SerializeField] private GameObject[] _allStartRooms = new GameObject[4];
    [SerializeField] private GameObject[] _allRooms = new GameObject[12];
    [SerializeField] private GameObject[] _allClosingRooms = new GameObject[4];
    [SerializeField] private GameObject _exitRoom;

    private int _closingRoomsQuantity;
    private int _randomStartRoom;
    private int _randomRoomsQuantity;
    private int[] _roomsType;


    private GameObject _grid;

    private void Start()
    {
        _grid = FindObjectOfType<Grid>().gameObject;
        RoomGenrationLogic();
    }
    
    private void RoomGenrationLogic()
    {
        //SetType
        _randomStartRoom = Random.Range(0, _allStartRooms.Length);


        //SetQuantity
        _randomRoomsQuantity = Random.Range(2, 10);

        //Set roomsType lenght
        _roomsType = new int[_randomRoomsQuantity];
        for(int i = 0; i < _randomRoomsQuantity; i++)
        {
            _roomsType[i] = Random.Range(0, 13);
        }

        //SetClosingRoomsQuantity
        for (int i = 0; i < _roomsType.Length; i++)
        {
            if (_allRooms[_roomsType[i]].GetComponent<RoomsData>().roomExits >= 1)                                     
            {
                print("кол-во выходов -- " + _allRooms[_roomsType[i]].GetComponent<RoomsData>().roomExits);
                _closingRoomsQuantity += _allRooms[_roomsType[i]].GetComponent<RoomsData>().roomExits;
            }
        }

        //sum
        int SumRooms = 1 + _roomsType.Length + _closingRoomsQuantity;
    }*/
   
}
