using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;
    private PlayerMove _playerMove;
    private Player _player;
    private PlayerAttack _playerAttack;

    private void Start()
    {
        _playerMove = GetComponent<PlayerMove>();
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
        _playerAttack = GetComponent<PlayerAttack>();
    }

    private void FixedUpdate()
    {
        RunAnimation(_playerMove.direction.x, _playerMove.direction.y);
        _player.onDamagePlayerEvent.AddListener(PlayDamagePlayerAnimation);
        _playerAttack.OnAttackEvent.AddListener(PlayAttackAnimation);
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
    private void PlayAttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }
}
