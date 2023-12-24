using Cinemachine;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    private RoomsData _room;
    private CinemachineVirtualCamera _camera;
    private Transform _lookAt;
    private bool _spawnEnemys;
    private void Awake()
    {
        _spawnEnemys = true;
        _room = gameObject.GetComponent<RoomsData>();
        _camera = FindObjectOfType<CinemachineVirtualCamera>();
        _lookAt = transform.GetChild(3).gameObject.transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            switch (_room.roomType)
            {
                case 0:
                    print("оно не есть комната");
                    break;
                case 2:
                    OnEnterRoom(_camera, _lookAt, _room.enemy);
                    break;
                case 3:
                    OnEnterRoom(_camera, _lookAt);
                    break;
                case 5:
                    OnEnterRoom(_camera, _lookAt);
                    break;
            }


        }
    }

    private void OnEnterRoom(CinemachineVirtualCamera Camera, Transform LookAt)
    {
        Camera.LookAt = LookAt;
        Camera.Follow = LookAt;
    }
    private void OnEnterRoom(CinemachineVirtualCamera Camera, Transform LookAt, Enemy Enemys)
    {
        if (_spawnEnemys == false) return;
        Vector2 RoomPos = transform.position;
        print(RoomPos);
        Camera.LookAt = LookAt;
        Camera.Follow = LookAt;
        for (int i = 0; i < 4; i++)
        {
            Enemy Enemy = Instantiate(Enemys, transform.parent = gameObject.transform);
            Enemy.transform.localPosition = Vector2.zero;
        }
        _spawnEnemys = false;
    }
}
