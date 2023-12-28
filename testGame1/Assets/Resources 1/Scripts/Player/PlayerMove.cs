using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    private float _moveSpeed = 7;

    private Rigidbody2D _rb;
    private SpriteRenderer _playerSpriteRotate;
    private Vector2 _direction;

    public SpriteRenderer playerSpriteRotate => _playerSpriteRotate;

    public Vector2 direction => _direction; 

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerSpriteRotate = GetComponent<SpriteRenderer>();
    }
    private void OnMove(InputValue value)
    {
        if(_rb.velocity == new Vector2(0, 0))
        {
            gameObject.transform.DORotate(new Vector3(0, 0, 0), 0.2f);
        }
        _direction = value.Get<Vector2>();
        _rb.velocity = new Vector2(_direction.x * _moveSpeed, _direction.y * _moveSpeed);
        gameObject.transform.DORotate(new Vector3(_direction.y * 30, _direction.x * 30, 0), 0.2f);
    }
}
