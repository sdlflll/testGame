using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    //rooms
    private RoomScript _thisRoom;
    private RoomsData _room;
    private Door _doorLeft;
    private Door _doorRight;
    private Door _doorUp;
    private Door _doorDown;
   

    //camera
    private CinemachineVirtualCamera _camera;
    private Transform _lookAt;
    private bool _toSpawnEnemys;

    //enimies
    public int enemiesCount;
    private List<Enemy> _enemies;

    private void Start()
    {
        _thisRoom = gameObject.GetComponent<RoomScript>();
       _toSpawnEnemys = true;
        //rooms
        _room = gameObject.GetComponent<RoomsData>();
        _doorLeft = _room.doorLeft.GetComponent<Door>();
        _doorRight = _room.doorRight.GetComponent<Door>();
        _doorUp = _room.doorUp.GetComponent<Door>();
        _doorDown = _room.doorDown.GetComponent<Door>();

        //camera
        _camera = FindObjectOfType<CinemachineVirtualCamera>();
        _lookAt = transform.GetChild(0).gameObject.transform;

    }

    private void FixedUpdate()
    {
        if(_toSpawnEnemys == false && enemiesCount != -1)
        {
            EnemyRoomQuest();
        }
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
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            OnExitRoom();
        }
    }

    private void OnEnterRoom(CinemachineVirtualCamera Camera, Transform LookAt)
    {
        Camera.LookAt = LookAt;
        Camera.Follow = LookAt;
        _room.mySprite.canvasRoomImage.color = new Color(1f, 1f, 1f, 0.5f);
    }
    private void OnExitRoom()
    {
        _room.mySprite.canvasRoomImage.color = new Color(1f, 1f, 1f, 0.15f);
    }
    private void OnEnterRoom(CinemachineVirtualCamera Camera, Transform LookAt, Enemy Enemy)
    {
        Camera.LookAt = LookAt;
        Camera.Follow = LookAt;
        _room.mySprite.canvasRoomImage.color = new Color(1f, 1f, 1f, 0.5f);
        if (_toSpawnEnemys == false) return;
        enemiesCount = Random.Range(3, 6);
        _enemies = new List<Enemy>(enemiesCount);
        for (int i = 0; i < enemiesCount; i++)
        { 
            Enemy NewEnemy = Instantiate(Enemy);
            NewEnemy.transform.localPosition = new Vector2(Random.Range(transform.position.x, transform.position.x + 7), Random.Range(transform.position.y, transform.position.y - 7));
            NewEnemy.room = _thisRoom;
            _enemies.Add(NewEnemy );
        }
        _doorLeft.doorAnimator?.SetTrigger("Close");
        _doorRight.doorAnimator?.SetTrigger("Close");
        _doorUp.doorAnimator?.SetTrigger("Close");
        _doorDown.doorAnimator?.SetTrigger("Close");
        _doorLeft.doorCollider.enabled = true;
        _doorRight.doorCollider.enabled = true;
        _doorUp.doorCollider.enabled = true;
        _doorDown.doorCollider.enabled = true;
        print("DoorsClose");

        _toSpawnEnemys = false;
    }
    private void EnemyRoomQuest()
    {
        if(enemiesCount == 0)
        {
            print("ты всех убил");
            _doorLeft.doorAnimator?.SetTrigger("Open");
            _doorRight.doorAnimator?.SetTrigger("Open");
            _doorUp.doorAnimator?.SetTrigger("Open");
            _doorDown.doorAnimator?.SetTrigger("Open");
            _doorLeft.doorCollider.enabled = false;
            _doorRight.doorCollider.enabled = false;
            _doorUp.doorCollider.enabled = false;
            _doorDown.doorCollider.enabled = false;
            enemiesCount--;
        }
        
    }
}                                                   