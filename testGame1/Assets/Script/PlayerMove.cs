using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEditor.Experimental.GraphView;

public class PlayerMove : MonoBehaviour
{
    public float MoveSpeed = 4;
    private Player PlayerMainControls;
    private Rigidbody2D _rb;
    private SpriteRenderer _playerSpriteRotate;
    private Vector2 _direction;
    private float _dashForce = 400;

    public Vector2 Direction => _direction; 

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerSpriteRotate = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        RotateAnimationHandler();
    }
    private void OnMove(InputValue value)
    {
        if(_rb.velocity == new Vector2(0, 0))
        {
            gameObject.transform.DORotate(new Vector3(0, 0, 0), 0.2f);
        }
        _direction = value.Get<Vector2>();
        _rb.velocity = new Vector2(_direction.x * MoveSpeed, _direction.y * MoveSpeed);
        gameObject.transform.DORotate(new Vector3(_direction.y * 30, _direction.x * 30, 0), 0.2f);
    }
    private void OnDash()
    {
        if (_rb.velocity == new Vector2(0, 0)) return;
        _rb.AddForce(new Vector2(_direction.x, _direction.y) * _dashForce, ForceMode2D.Impulse);
    }
    private void RotateAnimationHandler()
    {

        if (_direction.x > 0) 
        {
            _playerSpriteRotate.flipX = false;
        }
        else if(_direction.x < 0)
        {
            _playerSpriteRotate.flipX = true;
        }
    }
}
