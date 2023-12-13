using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using DG.Tweening;
using DG.Tweening.Core.Easing;

public class PlayerMove : MonoBehaviour
{
    private Player PlayerMainControls;
    private Rigidbody2D _rb;
    private SpriteRenderer _playerSpriteRotate;
    private Vector2 _direction;
    private float _moveSpeed = 2;
    private float _dashForce = 500;

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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _moveSpeed = 4;
        }
        else if(_rb.velocity == new Vector2(0, 0))
        {
            gameObject.transform.DORotate(new Vector3(0, 0, 0), 0.2f);
        }
        else
        {
            _moveSpeed = 2;
        }
        _direction = value.Get<Vector2>();
        _rb.velocity = new Vector2(_direction.x * _moveSpeed, _direction.y * _moveSpeed);
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
