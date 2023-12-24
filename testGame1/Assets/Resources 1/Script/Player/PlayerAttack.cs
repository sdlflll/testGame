using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private ParticleSystem _leftPlayerAttackPS;
    private ParticleSystem _rightPlayerAttackPS;

    public float attackDuration;
    public float attackRadius;
    public float attackDamage;
    private Collider2D[] _objectsOnCircle;
    private PlayerMove _playerMove;
 
    private void Awake()
    {
        _leftPlayerAttackPS = transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
        _rightPlayerAttackPS = transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();
        attackDamage = 20;
        attackRadius = 0.3f;
        _playerMove = GetComponent<PlayerMove>();
    }
    private void FixedUpdate()
    {
        if(attackDuration < 0.5f) attackDuration += Time.deltaTime;
    }
    private void OnAttack()
    {
        if (attackDuration <= 0.5f) return;
        attackDuration = 0;
        if (_playerMove.playerSpriteRotate.flipX == false)
        {
            _rightPlayerAttackPS.Play();
            _objectsOnCircle = Physics2D.OverlapCircleAll(new Vector2(transform.position.x + 0.5f, transform.position.y), attackRadius);
        }
        else
        {
            _leftPlayerAttackPS.Play();
            _objectsOnCircle = Physics2D.OverlapCircleAll(new Vector2(transform.position.x - 0.5f, transform.position.y), attackRadius);
        }
        for (int i = 0; i < _objectsOnCircle.Length; i++) //цикл проверяет все элементы в массиве objectsOnCircle, далее делает проверку на имения у елемента массива компонента "Enemy", если условие выплняется, то призывается функция GetDamage
        {
            print(_objectsOnCircle[i].gameObject.name);

            if (_objectsOnCircle[i].gameObject.GetComponent<Enemy>() && _objectsOnCircle[i].isTrigger == false)
            {
                print(attackDamage);
                _objectsOnCircle[i].GetComponent<Enemy>().GetDamage(attackDamage);
            }
        }

    }

}
