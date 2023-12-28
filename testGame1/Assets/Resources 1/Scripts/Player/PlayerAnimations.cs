using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;
    private PlayerMove _playerMove;
    private Player _player;

    private void Start()
    {
        _playerMove = GetComponent<PlayerMove>();
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        RunAnimation(_playerMove.direction.x, _playerMove.direction.y);
        _player.onDamagePlayerEvent.AddListener(PlayDamagePlayerAnimation);
    }

    private void RunAnimation(float x, float y)
    {
        _animator.SetFloat("x", x);
        _animator.SetFloat("y", y);
    }
    private void PlayDamagePlayerAnimation()
    {
        _animator.SetTrigger("Damage");
    }
}
