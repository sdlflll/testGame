using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    public UnityEvent OnAttackEvent = new UnityEvent();

    [SerializeField] private Ball _ball;
    public float attackDuration;
    public float attackRadius;
    public float attackDamage;
    private Vector3 _direction;


    private void Awake()
    {
        attackDamage = 20;
        attackRadius = 0.3f;
    }
    private void FixedUpdate()
    {
        if(attackDuration < 0.5f) attackDuration += Time.deltaTime;
    }
    private void OnAttack()
    {
        if (attackDuration <= 0.5f ) return;
        attackDuration = 0;
        OnAttackEvent.Invoke();
        _direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position ;
        float X = Mathf.FloorToInt(_direction.x);
        float Y = Mathf.FloorToInt(_direction.y);
        Vector2 Direction = new Vector2(X, Y).normalized;
        Ball NewBall = Instantiate(_ball, transform.position, Quaternion.identity);
        if(Direction != Vector2.zero) NewBall.ballRb.AddForce(Direction * 10, ForceMode2D.Impulse);
    }
}
