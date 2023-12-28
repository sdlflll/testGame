using UnityEngine;

[CreateAssetMenu(menuName = "StatsData/EnemyData", fileName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public float enemyHeart;
    public float enemyAttackDamage;
    public float enemySpeed;
    public float enemyAttackRadius;
    public bool longRangeAttack;
}
