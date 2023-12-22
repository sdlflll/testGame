using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    private CinemachineVirtualCamera Camera;
    private GameObject LookAt;
    private void Awake()
    {
        Camera = FindObjectOfType<CinemachineVirtualCamera>();
        LookAt = transform.GetChild(3).gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Camera.Follow = LookAt.transform;
            Camera.LookAt = LookAt.transform;
        }
    }
}
