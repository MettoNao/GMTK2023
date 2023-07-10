using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateManager : MonoBehaviour
{
    public EnemyCore CreateEnemy(EnemyCore enemy)
    {
        return Instantiate(enemy);
    }
}