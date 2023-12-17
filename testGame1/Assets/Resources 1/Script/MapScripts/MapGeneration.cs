using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    [SerializeField] private GameObject[] _allStartRooms;
    [SerializeField] private GameObject[] _allRooms;
    [SerializeField] private GameObject _exitRoom;
    private bool _roomGenerationLogic;

    private int _randomStartRoom;
    private int _randomRoomsQuantity;
    private int[] _roomsType;

    private GameObject _grid;


    //rotation

    private int _randomEndRoomRotation;

    private void Start()
    {
        _grid = FindObjectOfType<Grid>().gameObject;
        RoomGenrationLogic();
    }
    
    private void RoomGenrationLogic()
    {
        //Type
        _randomStartRoom = Random.Range(0, _allStartRooms.Length);


        //Quantity
        _randomRoomsQuantity = Random.Range(2, 6);
        _roomsType = new int[_randomRoomsQuantity];
        //rotation

        _randomEndRoomRotation = Random.Range(0, 5);

        for(int i = 0; i < _randomRoomsQuantity; i++)
        {
            _roomsType[i] = Random.Range(0, 5);
        }

        print("StartRoomType = " + _randomStartRoom + ";" +
            "" + "EndRoomRotation: " + _randomEndRoomRotation + ";" +
            "" + "RoomsQuantity: " + _randomRoomsQuantity + ";");

        for (int i = 0; i < _randomRoomsQuantity; i++) { print("#" + i +" RoomType: " + _roomsType[i] + ";"); }


        Instantiate(_allStartRooms[_randomStartRoom], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), _grid.transform);

        for (int i = 0; i < _randomRoomsQuantity; i++)
        {
        }
    }
   
}
