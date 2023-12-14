using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;
    private PlayerMove _playerMove;

    private void Start()
    {
        _playerMove = GetComponent<PlayerMove>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        SetRun(true ? _playerMove.Direction != new Vector2(0,0): false);
    }

    private void SetRun(bool Run)
    {
        _animator.SetBool("Run", Run);
    }
}
