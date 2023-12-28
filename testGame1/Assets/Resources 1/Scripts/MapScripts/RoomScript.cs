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
        _lookAt = transform.GetChild(0).gameObject.transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            switch (_room.roomType)
            {
                case 0:
                    break;
                case 1:
                    OnEnterRoom(_camera, _lookAt);
                    break;
                case 2:
                    OnEnterRoom(_camera, _lookAt);
                    break;
                case 3:
                    OnEnterRoom(_camera, _lookAt, _room.enemy);
                    break;
                case 4:
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
        Camera.LookAt = LookAt;
        Camera.Follow = LookAt;
        if (_spawnEnemys == false) return;
        Vector2 RoomPos = transform.position;
        print(RoomPos);
        for (int i = 0; i < 4; i++)
        {
            Enemy Enemy = Instantiate(Enemys, transform.parent = gameObject.transform);
            Enemy.transform.localPosition = new Vector2(Random.Range(0,7),Random.Range(0,-7));
        }
        _spawnEnemys = false;
    }
}
