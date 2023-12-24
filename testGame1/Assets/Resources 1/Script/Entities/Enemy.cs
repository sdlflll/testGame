using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour, IDamageble
{

    public UnityEvent OnDamageEvent = new UnityEvent();

    public EnemyData enemyData;
    public ParticleSystem leftAttackPS;
    public ParticleSystem rightAttackPS;
    public ParticleSystem getDamagePS;

    public float enemyHeart;
    public float enemyAttackDamage;
    public float enemySpeed;
    public float enemyAttackDuration;
    public bool playerInZone;
    public bool playerInAttackZone;
    public SpriteRenderer enemySpriteRotate;
    public Player player;
    public Animator enemyAnimator;
    public Enemy enemy;

    void Awake()
    {
        enemy = gameObject.GetComponent<Enemy>();
        enemyAnimator = GetComponent<Animator>();
        leftAttackPS = transform.GetChild(0).transform.GetChild(1).GetComponent<ParticleSystem>();
        rightAttackPS = transform.GetChild(0).transform.GetChild(2).GetComponent<ParticleSystem>();
        getDamagePS = transform.GetChild(0).transform.GetChild(0).GetComponent<ParticleSystem>();
        player = FindObjectOfType<Player>().GetComponent<Player>();

        enemyHeart = enemyData.enemyHeart;
        enemyAttackDamage = enemyData.enemyAttackDamage;
        enemySpeed = enemyData.enemySpeed;

        Initialize();
    }

    protected abstract void Initialize();

    public void GetDamage(float Damage)
    {
        OnDamageEvent.Invoke();
        getDamagePS.Play();
        enemyHeart -= Damage;
        DeathHendler(enemyHeart);
    }
    public void PlayDamageAnimation()
    {
        enemyAnimator.SetTrigger("Damage");

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
       Vector2 PlayerPosition = player.transform.position;

        float Step = Time.deltaTime * EnemySpeed; // скорость передвижения врага к игроку 
        EnemySpriteRotate.flipX = true ? PlayerPosition.x < transform.localPosition.x : false;
        gameObject.transform.position = Vector2.MoveTowards(transform.position, PlayerPosition, Step);
    }


    public void EnemyAttackAnimation(SpriteRenderer EnemySpriteRotate, ParticleSystem LeftAttackPS, ParticleSystem RightAttackPS)
    {
        if (EnemySpriteRotate.flipX == true) LeftAttackPS.Play();
        else RightAttackPS.Play(); 
    }
    public bool AttackPlayer(float EnemyAttackDamage, SpriteRenderer EnemySpriteRotate, ParticleSystem LeftAttackPS, ParticleSystem RightAttackPS, float EnemyAttackDuration, Player Player, bool AttackZone)
    {
        if (AttackZone == true)
        {
            EnemyAttackAnimation(EnemySpriteRotate, LeftAttackPS, RightAttackPS);
            Player.GetDamage(EnemyAttackDamage);
            return true;
        }
        return false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInZone = true;
            player = collision.gameObject.GetComponent<Player>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInZone = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInAttackZone = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInAttackZone = false;
        }
    }


    //PlayerDetector... хе-хе

}
