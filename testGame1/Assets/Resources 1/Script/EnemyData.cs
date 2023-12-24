using UnityEngine;

[CreateAssetMenu(menuName = "StatsData/EnemyData", fileName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public float enemyHeart;
    public float enemyAttackDamage;
    public float enemySpeed;
}
