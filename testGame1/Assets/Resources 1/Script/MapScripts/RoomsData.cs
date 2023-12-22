using UnityEngine;

public class RoomsData : MonoBehaviour
{
    public int roomExits;
    public int roomType;
    public int ySize;
    public int xSize;
    public bool[] exitsDirections = new bool[4];
    // ED[1] == top ED[2] == right ED[3] == bottom ED[2] == left
    // roomTypes: 0 = noneRoom; 1 = startRoom; 2 = Room ; 3 = ExitRoom; 4 = closingDoor;
    // roomExits: 0 = noneExits; 1 = 1 Exit; 2 = 2Exit; 3 = 3 Exit ; 4 = 4 Exit;

}
