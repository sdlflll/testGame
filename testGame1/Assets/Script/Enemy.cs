using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IDamageble
{
    [SerializeField] private ParticleSystem _leftAttackPS;
    [SerializeField] private ParticleSystem _rightAttackPS;
    [SerializeField] private ParticleSystem _getDamagePS;

    private float _enemyHeart = 50;
    private float _enemyAttack = 15;
    private float _enemySpeed = 2.5f;
    private float _duration;
    private bool _playerInZone;
    private Vector2 _playerPosition;
    private SpriteRenderer _enemySpriteRotate;
    private Player _player;

    void Start()
    {
        _enemySpriteRotate = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        DeathHendler();
        if (_playerInZone) FindPlayer(); AttackPlayer();
        
    }

    public void GetDamage(float Damage)
    {
        _getDamagePS.Play();
        _enemyHeart -= Damage;
    }

    private void DeathHendler()
    {
        if(_enemyHeart <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void FindPlayer()
    {
        float Step = Time.deltaTime * _enemySpeed; // скорость передвижения врага к игроку 
        _playerPosition = FindObjectOfType<Player>().transform.position;
        _enemySpriteRotate.flipX = true ? _playerPosition.x < transform.localPosition.x : false;
        gameObject.transform.position = Vector2.MoveTowards(transform.position, _playerPosition, Step);
    }
    private void EnemyAttackAnimation()
    {
        if (_enemySpriteRotate.flipX == true) _leftAttackPS.Play();
        else
        {
            _rightAttackPS.Play();
            print("правой бей");
        } 
    }
    private void AttackPlayer()
    {
        _duration += Time.deltaTime;
        if(_duration >= 2 && _player != null)
        {
            EnemyAttackAnimation();
            _player.GetDamage(_enemyAttack);
            _duration = 0;
        }
    }

    //PlayerDetector... хе-хе
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _playerInZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _playerInZone = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _player = collision.gameObject.GetComponent<Player>();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _player = null;
        }
    }

}
