using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUpdate
{
    private float timer;
    private GetNearistEnemyScript getNearistEnemyScript;

    PlayerCore player;
    List<EnemyCore> allEnemies = new List<EnemyCore>();
    List<LevelData> levels = new List<LevelData>();
    public EnemyUpdate(PlayerCore _player, List<EnemyCore> all_enemies, List<LevelData> _levels)
    {
        timer = 3.0f;
        getNearistEnemyScript = new GetNearistEnemyScript();
        player = _player;
        allEnemies = all_enemies;
        levels = _levels;
    }

    // Update is called once per frame
    public void OnEnemyUpdate(int level)
    {
        if (allEnemies.Count <= 0) return;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            int count = Random.Range(1, levels[level].enemyAttackCount);

            List<int> indexies = new List<int>();
            for (int i = 0; i < allEnemies.Count; i++)
            {
                indexies.Add(i);
            }

            for (int i = 0; i < count; i++)
            {
                int indexIndex = Random.Range(0, indexies.Count);

                if (indexies.Count <= indexIndex)
                {
                    break;
                }

                int index = indexies[indexIndex];

                if (allEnemies[index].isDeath == true)
                {
                    continue;
                }

                allEnemies[index].Shoot();

                indexies.RemoveAt(indexIndex);

                if (indexies.Count < 0)
                {
                    break;
                }
            }

            timer = Random.Range(levels[level].minAttackInterval, levels[level].maxAttackInterval);
        }
    }

    public void OnEnemyFixedUpdate()
    {
        if (player.GetIsDeath == true) return;

        if (allEnemies.Count <= 0) return;

        foreach (var e in allEnemies)
        {
            var allyTarget = getNearistEnemyScript.GetNearistEnemy(e.transform.position, allEnemies);
            e.Move(allyTarget, player.transform);
        }
    }
}
