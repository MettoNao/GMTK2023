using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetNearistEnemyScript
{
    //一番近い敵のTransformを返す
    public Transform GetNearistEnemy(Vector2 pos, List<EnemyCore> allEnemies)
    {

        float tmpDis = 0;
        float nearDis = 0;
        Transform target = null;

        foreach (EnemyCore obs in allEnemies)
        {
            if (obs.isDeath == true || obs.getIsAlly == true) continue;
            tmpDis = Vector3.Distance(obs.transform.position, pos);

            if (nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                target = obs.transform;
            }

        }

        return target;
    }
}
