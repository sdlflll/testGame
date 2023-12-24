using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Skeleton : Enemy
{
    protected override void Initialize()
    {
        enemyAttackDuration = 0;
        enemySpriteRotate = GetComponent<SpriteRenderer>();
    }


    private void FixedUpdate()
    {
        if (playerInZone) 
        {
            enemyAttackDuration += Time.deltaTime;
            if(enemyAttackDuration >= 2)
            {
                if(AttackPlayer(enemyData.enemyAttackDamage, enemySpriteRotate, leftAttackPS, rightAttackPS, enemyAttackDuration, player, playerInAttackZone))
                {
                    enemyAttackDuration = 0;
                }
                else if(enemyAttackDamage >= 3)
                {
                    enemyAttackDuration = 0;
                }
            }
            FindPlayer(enemySpriteRotate, enemyData.enemySpeed);
        };
        enemy.OnDamageEvent.AddListener(PlayDamageAnimation);
    }



   



}
