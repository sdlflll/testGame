using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IDamageble
{

    public UnityEvent OnDamageEvent = new UnityEvent();

    public EnemyData enemyData;
    public ParticleSystem leftAttackPS;
    public ParticleSystem rightAttackPS;
    public ParticleSystem getDamagePS;

    private string _enemyName;
    private float _enemyHeart;
    private float _enemyAttackDamage;
    private float _enemySpeed;
    private float _enemyAttackDuration;
    private float _attackRadius;
    private bool _playerInZone;
    private bool _playerInAttackZone;
    private bool _longRangeAttack;
    private SpriteRenderer _enemySpriteRotate;
    private Player _player;
    private Animator _enemyAnimator;
    private Enemy _enemy;
    private Rigidbody2D _enemyRb;

    void Awake()
    {
        _enemyRb = GetComponent<Rigidbody2D>();
        _enemySpriteRotate = GetComponent<SpriteRenderer>();
        _enemy = gameObject.GetComponent<Enemy>();
        _enemyAnimator = GetComponent<Animator>();
        leftAttackPS = transform.GetChild(0).transform.GetChild(1).GetComponent<ParticleSystem>();
        rightAttackPS = transform.GetChild(0).transform.GetChild(2).GetComponent<ParticleSystem>();
        getDamagePS = transform.GetChild(0).transform.GetChild(0).GetComponent<ParticleSystem>();
        _player = FindObjectOfType<Player>().GetComponent<Player>();

        //придаю значения переменным
        _enemyName = enemyData.enemyName;
        _enemyHeart = enemyData.enemyHeart;
        _enemyAttackDamage = enemyData.enemyAttackDamage;
        _enemySpeed = enemyData.enemySpeed;
        _attackRadius = enemyData.enemyAttackRadius;
        _longRangeAttack = enemyData.longRangeAttack;
    }

    private void FixedUpdate()
    {
        if (_playerInZone)
        {
            _enemyAttackDuration += Time.deltaTime;
            if (_enemyAttackDuration >= 1.5f)
            {
                if (EnemyAttack(enemyData.enemyAttackDamage, _enemySpriteRotate, leftAttackPS, rightAttackPS, _player, _playerInAttackZone, _longRangeAttack))
                {
                    _enemyAttackDuration = 0;
                }
                else if (_enemyAttackDamage >= 3)
                {
                    _enemyAttackDuration = 0;
                }
            }
            FindPlayer(_enemySpriteRotate, enemyData.enemySpeed);
        };
        _enemy.OnDamageEvent.AddListener(PlayDamageAnimation);
    }



    public void GetDamage(float Damage)
    {
        OnDamageEvent.Invoke();
        getDamagePS.Play();
        _enemyHeart -= Damage;
        DeathHendler(_enemyHeart);
    }
    public void PlayDamageAnimation()
    {
        _enemyAnimator.SetTrigger("Damage");

    }

    public void DeathHendler(float EnemyHealth)
    {
        if(EnemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }


    public void FindPlayer(SpriteRenderer EnemySpriteRotate, float EnemySpeed)
    {
       Vector2 PlayerPosition = _player.transform.position;

        float Step = Time.deltaTime * EnemySpeed; // скорость передвижения врага к игроку 
        EnemySpriteRotate.flipX = true ? PlayerPosition.x < transform.localPosition.x : false;
        gameObject.transform.position = Vector2.MoveTowards(transform.position, PlayerPosition, Step);
    }


    public void EnemyAttackAnimation(SpriteRenderer EnemySpriteRotate, ParticleSystem LeftAttackPS, ParticleSystem RightAttackPS)
    {
        if (EnemySpriteRotate.flipX == true) LeftAttackPS.Play();
        else RightAttackPS.Play(); 
    }
    public bool EnemyAttack(float EnemyAttackDamage, SpriteRenderer EnemySpriteRotate, ParticleSystem LeftAttackPS, ParticleSystem RightAttackPS, Player Player, bool AttackZone, bool longRangeAttack)
    {
        if (longRangeAttack)
        {
            if(_playerInZone)
            {
                Collider2D[] FireRadius = Physics2D.OverlapCircleAll(transform.position, _attackRadius);
                for (int i = 0; i < FireRadius.Length; i++)
                {
                    if (FireRadius[i].gameObject == _player)
                    {

                    }
                }
            }
           
        }
        else
        {
            if (AttackZone == true)
            {
                EnemyAttackAnimation(EnemySpriteRotate, LeftAttackPS, RightAttackPS);
                Player.GetDamage(EnemyAttackDamage);
                return true;
            }
        }
        return false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _playerInZone = true;
            _player = collision.gameObject.GetComponent<Player>();
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
        if (collision.gameObject.tag == "Player")
        {
            _playerInAttackZone = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _playerInAttackZone = false;
        }
    }


    //PlayerDetector... хе-хе

}
