using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Recorder.OutputPath;

public class MapGeneration : MonoBehaviour
{
    public RoomsData _room;
    private RoomsData[,] _spawnedRooms;
    [SerializeField] private RoomsData _startRoom;


    private Transform _grid;


    private void Start()
    {
        _spawnedRooms = new RoomsData[11, 11];
        _grid = GameObject.FindGameObjectWithTag("Grid").transform;
        RoomsData StartRoom = Instantiate(_startRoom, transform.parent = _grid);
        StartRoom.transform.position = Vector2.zero;
        StartRoom.doorUp.SetActive(true);
        StartRoom.doorDown.SetActive(true);
        StartRoom.doorLeft.SetActive(true);
        StartRoom.doorRight.SetActive(true);
        _spawnedRooms[5, 5] = StartRoom;
        for (int i = 0; i < 12; i++)
        {
            PlaceOneRoom();

        }
        ConnectToSomething(StartRoom, new Vector2Int(5, 5));
    }

    private void PlaceOneRoom()
    {
        HashSet<Vector2Int> vacantPlaces1 = new HashSet<Vector2Int>();
        for(int x = 0; x < _spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < _spawnedRooms.GetLength(1); y++)
            {

                print(_spawnedRooms.GetLength(0));
                print(_spawnedRooms.GetLength(1));

                if (_spawnedRooms[x, y] == null) continue;

                int maxX = _spawnedRooms.GetLength(0) - 1;
                int maxY = _spawnedRooms.GetLength(1) - 1;

                if (x > 0 && _spawnedRooms[x - 1, y] == null) vacantPlaces1.Add(new Vector2Int(x - 1, y));

                if (y > 0 && _spawnedRooms[x, y - 1] == null) vacantPlaces1.Add(new Vector2Int(x, y - 1));

                if (x < maxX && _spawnedRooms[x + 1 , y] == null) vacantPlaces1.Add(new Vector2Int(x + 1 , y));

                if (y < maxY && _spawnedRooms[x, y + 1] == null) vacantPlaces1.Add(new Vector2Int(x, y + 1));
            }
        }
        RoomsData newRoom = Instantiate(_room, transform.parent = _grid);
        newRoom.doorUp.SetActive(true);
        newRoom.doorDown.SetActive(true);
        newRoom.doorLeft.SetActive(true);
        newRoom.doorRight.SetActive(true);

        int limit = 500;

        while (limit-- > 0)
        {
            Vector2Int newRoomPosition = vacantPlaces1.ElementAt(Random.Range(0, vacantPlaces1.Count));
            if (ConnectToSomething(newRoom, newRoomPosition))
            {
                newRoom.transform.position = new Vector2(newRoomPosition.x - 5, newRoomPosition.y - 5) * 11;
                _spawnedRooms[newRoomPosition.x, newRoomPosition.y] = newRoom;
                break;
            }
        }
    }

    private bool ConnectToSomething(RoomsData room, Vector2Int p)
    {

        print(p);
        int maxX = _spawnedRooms.GetLength(0) - 1;
        int maxY = _spawnedRooms.GetLength(1) - 1;

        

        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (room.doorUp != null && p.y < maxY && _spawnedRooms[p.x, p.y + 1]?.doorDown != null) neighbours.Add(Vector2Int.up);
        if (room.doorDown != null && p.y > 0 && _spawnedRooms[p.x, p.y - 1]?.doorUp != null) neighbours.Add(Vector2Int.down);

        if (room.doorRight != null && p.x < maxX && _spawnedRooms[p.x + 1, p.y]?.doorLeft != null) neighbours.Add(Vector2Int.right);
        if (room.doorLeft != null && p.x > 0 && _spawnedRooms[p.x - 1, p.y]?.doorRight != null) neighbours.Add(Vector2Int.left);

        if (neighbours.Count == 0) return false;

        print(neighbours.Count);

        Vector2Int selectedDirection = neighbours[Random.Range(0, neighbours.Count)];
        RoomsData selectedRoom = _spawnedRooms[p.x + selectedDirection.x, p.y + selectedDirection.y];


        if (selectedDirection == Vector2Int.up)
        {
            room.doorUp.SetActive(false);
            selectedRoom.doorDown.SetActive(false);
            
        }
        else if (selectedDirection == Vector2Int.down)
        {
            room.doorDown.SetActive(false);
            selectedRoom.doorUp.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.right)
        {
            room.doorRight.SetActive(false);
            selectedRoom.doorLeft.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.left)
        {
            room.doorLeft.SetActive(false);
            selectedRoom.doorRight.SetActive(false);
        }
        return true;
    }




}
