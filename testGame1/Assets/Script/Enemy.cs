using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool PlayerInZone;
    public float EnemyHeart = 30;
    public float EnemyAttack = 15;
    private float _enemySpeed = 5;
    private Vector2 _playerPosition;
    private SpriteRenderer _enemySpriteRotate;
    void Start()
    {
        _enemySpriteRotate = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(PlayerInZone) FindPlayer();
    }

    private void FindPlayer()
    {
        float Step = Time.deltaTime * _enemySpeed;
        _playerPosition = FindObjectOfType<Player>().transform.position;
        gameObject.transform.position = Vector2.MoveTowards(transform.position, _playerPosition, Step);
        if(_playerPosition.x > transform.position.x)
        {
            _enemySpriteRotate.flipX = false;
        }
        else
        {
            _enemySpriteRotate.flipX = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerInZone = true;
        }
        else
        {
            PlayerInZone = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            PlayerInZone = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

        }
    }





}
