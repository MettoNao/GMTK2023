using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner
{
    private float timer;

    private List<List<EnemyCore>> spawnedEnemies = new List<List<EnemyCore>>();

    List<EnemyCore> allEnemies = new List<EnemyCore>();
    List<LevelData> levels = new List<LevelData>();
    InstantiateManager instantiateManager;
    public EnemySpawner(List<EnemyCore> all_enemies, List<LevelData> _levels, InstantiateManager _instantiateManager)
    {
        timer = 3.0f;
        camera = Camera.main;
        allEnemies = all_enemies;
        levels = _levels;
        instantiateManager = _instantiateManager;
    }

    // Update is called once per frame
    public void CreateEnemySequence(int level)
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            int count = Random.Range(1, levels[level].enemySpawnCount);
            for (int i = 0; i < count; i++)
            {
                var e = CreateEnemy(levels[level].enemies[Random.Range(0, levels[level].enemies.Count)]);
                e.transform.position = GetRandomSpawnPoint();
                e.Init();
            }

            timer = Random.Range(levels[level].minSpawnInterval, levels[level].maxSpawnInterval);
        }
    }

    public EnemyCore CreateEnemy(EnemyCore obj)
    {
        int listIndex = -1;
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            foreach (var o in spawnedEnemies[i])
            {
                if (o.name == obj.name)
                {
                    listIndex = i;
                    if (o.gameObject.activeSelf == false)
                    {
                        o.gameObject.SetActive(true);
                        return o;
                    }
                }
            }
        }

        if (listIndex != -1)
        {
            var o = instantiateManager.CreateEnemy(obj);
            o.name = obj.name;
            spawnedEnemies[listIndex].Add(o);
            allEnemies.Add(o);
            return o;
        }
        else
        {
            spawnedEnemies.Add(new List<EnemyCore>());
            var o = instantiateManager.CreateEnemy(obj);
            o.name = obj.name;
            spawnedEnemies[spawnedEnemies.Count - 1].Add(o);
            allEnemies.Add(o);
            return o;
        }
    }

    Camera camera;
    Vector2 GetRandomSpawnPoint()
    {
        Vector2 lb = camera.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 rt = camera.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 lt = new Vector3(lb.x, rt.y);
        Vector2 rb = new Vector3(rt.x, lb.y);
        return new Vector2(Random.Range(lt.x, rt.x), Random.Range(lb.y, lt.y));
    }
}