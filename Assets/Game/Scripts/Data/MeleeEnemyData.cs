
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Data/Melee enemy",fileName = "MeleeEnemyData")]
public class MeleeEnemyData : ScriptableObject
{
    public List<MeleeEnemyParam> MeleeEnemys;
}
[System.Serializable]
public class MeleeEnemyParam
{
    public string Name;
    public float HealthPoint;

    public float MoveSpeed;

    public float Damage;
    public float AttackRange;
    public float AttackSpeed;

    public int RewardGold;
    public GameObject EnemyPrefab;
}
