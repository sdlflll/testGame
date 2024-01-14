using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public CanvasRoomSprite _roomSprite;
    public RoomsData room;
    public RoomsData enemyRoom;
    public RoomsData weaponRoom;
    public RoomsData startRoom;
    private RoomsData[,] _spawnedRooms;

    private CanvasMapGeneration _canvas;

    private Transform _grid;

    private int _roomCount;
    private int _roomQuantity;


    private void Start()
    {
        _roomQuantity = Random.Range(5, 13);
        _canvas = FindObjectOfType<CanvasMapGeneration>();
        _spawnedRooms = new RoomsData[5, 5];
        _grid = GameObject.FindGameObjectWithTag("Grid").transform;
        RoomsData StartRoom = Instantiate(startRoom, transform.parent = _grid);
        StartRoom.transform.position = Vector2.zero;
        CanvasRoomSprite StartRoomSprite = Instantiate(_roomSprite, transform.parent = _canvas.canvas);
        StartRoomSprite.spriteRoomType = StartRoom.roomType;
        StartRoomSprite.transform.localPosition = _grid.position;
        StartRoom.mySprite = StartRoomSprite;

        _spawnedRooms[2, 2] = StartRoom;
        for (int i = 0; i < _roomQuantity; i++)
        {
            PlaceOneRoom();
        }
        ConnectToSomething(StartRoom, new Vector2Int(2, 2));
    }

    private void PlaceOneRoom()
    {
        _roomCount++;
        HashSet<Vector2Int> vacantPlaces1 = new HashSet<Vector2Int>();
        HashSet<Vector2Int> vacantSpritePlaces = new HashSet<Vector2Int>();
        for(int x = 0; x < _spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < _spawnedRooms.GetLength(1); y++)
            {
                if (_spawnedRooms[x, y] == null) continue;

                int maxX = _spawnedRooms.GetLength(0) - 1;
                int maxY = _spawnedRooms.GetLength(1) - 1;

                if (x > 0 && _spawnedRooms[x - 1, y] == null) {
                    vacantPlaces1.Add(new Vector2Int(x - 1, y));
                };

                if (y > 0 && _spawnedRooms[x, y - 1] == null)
                {
                    vacantPlaces1.Add(new Vector2Int(x, y - 1));
                };

                if (x < maxX && _spawnedRooms[x + 1, y] == null) 
                {
                    vacantPlaces1.Add(new Vector2Int(x + 1, y));
                };

                if (y < maxY && _spawnedRooms[x, y + 1] == null) 
                { 
                    vacantPlaces1.Add(new Vector2Int(x, y + 1)); 
                } ;
            }
        }
        RoomsData newRoom = Instantiate(RoomLogic(_roomCount), transform.parent = _grid);
        CanvasRoomSprite newRoomSprite = Instantiate(_roomSprite, transform.parent = _canvas.canvas);
        newRoomSprite.spriteRoomType = newRoom.roomType;
        newRoom.mySprite = newRoomSprite;
        int limit = 500;

        while (limit-- > 0)
        {
            Vector2Int newRoomPosition = vacantPlaces1.ElementAt(Random.Range(0, vacantPlaces1.Count));
            
            if (ConnectToSomething(newRoom, newRoomPosition))
            {
                newRoom.transform.position = new Vector2(newRoomPosition.x - 2, newRoomPosition.y - 2) * 11;
                newRoomSprite.transform.localPosition = new Vector2(newRoomPosition.x - 2, newRoomPosition.y - 2) * 15;
                newRoomSprite.gameObject.SetActive(false);
                _spawnedRooms[newRoomPosition.x, newRoomPosition.y] = newRoom;
                break;
            }
        }
    }

    private RoomsData RoomLogic(int roomCount)
    {
       if(roomCount == _roomQuantity - 2)
        {
            return weaponRoom;
        }
        else
        {
            return Random.Range(0, 8) > 2 ? enemyRoom : room;
        }
    }

    private bool ConnectToSomething(RoomsData room, Vector2Int p)
    {

        int maxX = _spawnedRooms.GetLength(0) - 1;
        int maxY = _spawnedRooms.GetLength(1) - 1;

        

        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (room.doorUp != null && p.y < maxY && _spawnedRooms[p.x, p.y + 1]?.doorDown != null) neighbours.Add(Vector2Int.up);
        if (room.doorDown != null && p.y > 0 && _spawnedRooms[p.x, p.y - 1]?.doorUp != null) neighbours.Add(Vector2Int.down);

        if (room.doorRight != null && p.x < maxX && _spawnedRooms[p.x + 1, p.y]?.doorLeft != null) neighbours.Add(Vector2Int.right);
        if (room.doorLeft != null && p.x > 0 && _spawnedRooms[p.x - 1, p.y]?.doorRight != null) neighbours.Add(Vector2Int.left);

        if (neighbours.Count == 0) return false;


        Vector2Int selectedDirection = neighbours[Random.Range(0, neighbours.Count)];
        RoomsData selectedRoom = _spawnedRooms[p.x + selectedDirection.x, p.y + selectedDirection.y];


        if (selectedDirection == Vector2Int.up)
        {
            room.doorUp.transform.GetChild(0).gameObject.SetActive(true);
            selectedRoom.doorDown.transform.GetChild(0).gameObject.SetActive(true);

            room.doorUp.transform.GetChild(1).gameObject.SetActive(false);    
            selectedRoom.doorDown.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.down)
        {
            room.doorDown.transform.GetChild(0).gameObject.SetActive(true);
            selectedRoom.doorUp.transform.GetChild(0).gameObject.SetActive(true);

            room.doorDown.transform.GetChild(1).gameObject.SetActive(false);    
            selectedRoom.doorUp.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.right)
        {
            room.doorRight.transform.GetChild(0).gameObject.SetActive(true);
            selectedRoom.doorLeft.transform.GetChild(0).gameObject.SetActive(true);

            room.doorRight.transform.GetChild(1).gameObject.SetActive(false);
            selectedRoom.doorLeft.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.left)
        {
            room.doorLeft.transform.GetChild(0).gameObject.SetActive(true);
            selectedRoom.doorRight.transform.GetChild(0).gameObject.SetActive(true); ;

            room.doorLeft.transform.transform.GetChild(1).gameObject.SetActive(false);
            selectedRoom.doorRight.transform.GetChild(1).gameObject.SetActive(false);
        }
        return true;
    }




}
