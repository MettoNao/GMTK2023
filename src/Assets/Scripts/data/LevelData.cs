using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/CreateLevelData")]
public class LevelData : ScriptableObject
{
    public int id;
    public float elapsedTime;
    public List<EnemyCore> enemies = new List<EnemyCore>();
    public float minSpawnInterval;
    public float maxSpawnInterval;
    public float minAttackInterval;
    public float maxAttackInterval;
    public int enemySpawnCount;
    public int enemyAttackCount;
}
