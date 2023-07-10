using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;
public class NormalHitEffect : AHitEvent
{
    [SerializeField] private GameObject effect;
    [SerializeField] private GameObject deathEffect;

    public override void HitEvent(bool isDeath)
    {
        if (isDeath)
        {
            SEManager.Instance.Play(SEPath.DETH);
            var e = ObjectPool.Instance.CreateEffect(deathEffect);
            e.transform.position = transform.position;
            CameraShaker.Instance.ShakeCamera(0.5f, 30, 1.0f);
        }
        else
        {
            SEManager.Instance.Play(SEPath.DAMAGE);
            var e = ObjectPool.Instance.CreateEffect(effect);
            e.transform.position = transform.position;
            CameraShaker.Instance.ShakeCamera(0.3f, 30, 0.5f);
        }
    }
}