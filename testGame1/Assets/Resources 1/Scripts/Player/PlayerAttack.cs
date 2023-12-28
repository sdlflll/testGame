using DG.Tweening;
using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
   
    [SerializeField] private Ball _ball;
    public float attackDuration;
    public float attackRadius;
    public float attackDamage;
    private Collider2D[] _objectsOnCircle;
    private PlayerMove _playerMove;
    private Vector3 _direction;


    private void Awake()
    {
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
        _direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ball NewBall = Instantiate(_ball, transform.position, new Quaternion(0,0,0,0));
        NewBall.ballRb.AddForce(-(transform.position - _direction ) * 100);
        Destroy(NewBall, 3);





        /* if (_playerMove.direction.x > 1)
         {
             _objectsOnCircle = Physics2D.OverlapCircleAll(new Vector2(transform.position.x + 0.5f, transform.position.y), attackRadius);
         }
         else
         {
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
         }*/

    }

}
