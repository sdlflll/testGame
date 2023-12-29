using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using static UnityEditor.Recorder.OutputPath;

public class Enemy : MonoBehaviour, IDamageble
{                                                              
    public UnityEvent OnEnemyDeathEvent = new UnityEvent();
    public UnityEvent OnEnemyDamageEvent = new UnityEvent();
    public UnityEvent OnEnemyAttackEvent = new UnityEvent();

    public EnemyData enemyData;

    public RoomScript room;
    private string _enemyName;
    private float _enemyHeart;
    private float _enemyAttackDamage;
    private float _enemySpeed;
    private float _enemyAttackDuration;
    private float _attackRadius;
    private float _attackDuration;
    private bool _playerInZone;
    private bool _playerInAttackZone;
    private bool _longRangeAttack;
    private SpriteRenderer _enemySpriteRotate;
    private Player _player;
    private Animator _enemyAnimator;
    private Enemy _enemy;
    private PlayableDirector _playerDirector;

    void Awake()
    {
        _playerDirector = GetComponent<PlayableDirector>();
        _enemySpriteRotate = GetComponent<SpriteRenderer>();
        _enemy = gameObject.GetComponent<Enemy>();
        _enemyAnimator = GetComponent<Animator>();
        _player = FindObjectOfType<Player>().GetComponent<Player>();

        //придаю значения переменным
        _enemyName = enemyData.enemyName;
        _enemyHeart = enemyData.enemyHeart;
        _enemyAttackDamage = enemyData.enemyAttackDamage;
        _enemySpeed = enemyData.enemySpeed;
        _attackRadius = enemyData.enemyAttackRadius;
        _attackDuration = enemyData.attackDuration;
        _longRangeAttack = enemyData.longRangeAttack;
    }

    private void FixedUpdate()
    {
        if (_playerInZone)
        {
            FindPlayer(_enemySpriteRotate, _enemySpeed);
            _enemyAttackDuration += Time.deltaTime;
            if (_enemyAttackDuration >= 1.2f)
            {
                if (EnemyAttack(enemyData.enemyAttackDamage,_player, _playerInAttackZone))
                {
                    _enemyAttackDuration = 0;
                }
                else if (_attackDuration >= 3)
                {
                    _enemyAttackDuration = 0;
                }
            }
        }
        _enemy.OnEnemyDamageEvent.AddListener(PlayEnemyDamageAnimation);
        _enemy.OnEnemyAttackEvent.AddListener(PlayEnemyAttackAnimation);
        PlayEnemyRunAnimation(_playerInZone);
    }

    public void DestroyEnemy()
    {
        room.enemiesCount--;
        Destroy(gameObject);
    }
    public void GetDamage(float Damage)
    {
        OnEnemyDamageEvent.Invoke();
        _enemyHeart -= Damage;
        DeathHendler(_enemyHeart);
    }
    private void DeathHendler(float EnemyHealth)
    {
        if (EnemyHealth <= 0)
        {
            _playerDirector.Play();
        }
    }

    //animations
    private void PlayEnemyDamageAnimation()
    {
        _enemyAnimator.SetTrigger("Damage");

    }

    private void PlayEnemyAttackAnimation()
    {
        _enemyAnimator.SetTrigger("Attack");
    }

    private void PlayEnemyRunAnimation(bool Run)
    {
        _enemyAnimator.SetBool("EnemyWalk", Run); 
    }
    
    //FindPlayer || PLayerAttack
    private void FindPlayer(SpriteRenderer EnemySpriteRotate, float EnemySpeed)
    {
       Vector2 PlayerPosition = _player.transform.position;
       float Step = Time.deltaTime * EnemySpeed; // скорость передвижения врага к игроку 
       EnemySpriteRotate.flipX = true ? PlayerPosition.x < transform.localPosition.x : false;
       gameObject.transform.position = Vector2.MoveTowards(transform.position, PlayerPosition, Step);
    }

    private bool EnemyAttack(float EnemyAttackDamage, Player Player, bool AttackZone)
    {
        if (AttackZone == true)
            {
                OnEnemyAttackEvent.Invoke();
                Player.GetDamage(EnemyAttackDamage);
                return true;
            }
        else return false;
    }


    //Collisions and triggers
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
