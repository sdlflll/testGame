using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float _attackRadius = 1;
    private float _attackDamage = 20;

    void OnAttack()
    {
        Collider2D[] ObjectsOnCircle = Physics2D.OverlapCircleAll(transform.position, _attackRadius);
        for(int i = 0; i < ObjectsOnCircle.Length; i++)
        {
           /* if (ObjectsOnCircle[i].gameObject.)
            {
                ObjectsOnCircle[i].GetComponent<Damageble>().
            }*/
        }

        
    }


}
