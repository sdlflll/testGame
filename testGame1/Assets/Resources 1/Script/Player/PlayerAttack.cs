using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private ParticleSystem _leftPlayerAttackPS;
    [SerializeField] private ParticleSystem _rightPlayerAttackPS;

    private float _attackDuration;
    private float _attackRadius = 0.3f;
    private float _attackDamage = 10;
    private Collider2D[] _objectsOnCircle;
    private PlayerMove _playerMove;
 
    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
    }
    private void FixedUpdate()
    {
        _attackDuration += Time.deltaTime;
    }
    private void OnAttack()
    {
        if (_attackDuration <= 1.5f) return;
        _attackDuration = 0;
        if (_playerMove.playerSpriteRotate.flipX == false)
        {
            _rightPlayerAttackPS.Play();
            _objectsOnCircle = Physics2D.OverlapCircleAll(new Vector2(transform.position.x + 0.5f, transform.position.y), _attackRadius);
        }
        else
        {
            _leftPlayerAttackPS.Play();
            _objectsOnCircle = Physics2D.OverlapCircleAll(new Vector2(transform.position.x - 0.5f, transform.position.y), _attackRadius);
        }
        for (int i = 0; i < _objectsOnCircle.Length; i++) //цикл проверяет все элементы в массиве objectsOnCircle, далее делает проверку на имения у елемента массива компонента "Enemy", если условие выплняется, то призывается функция GetDamage
        {
            print(_objectsOnCircle[i].gameObject.name);

            if (_objectsOnCircle[i].gameObject.GetComponent<Enemy>() && _objectsOnCircle[i].isTrigger == false)
            {
                print(_attackDamage);
                _objectsOnCircle[i].GetComponent<Enemy>().GetDamage(_attackDamage);
            }
        }

    }

}
