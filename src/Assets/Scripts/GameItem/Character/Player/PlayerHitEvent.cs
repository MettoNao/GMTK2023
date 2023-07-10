using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;
public class PlayerHitEvent : AHitEvent
{
    [SerializeField] private GameObject effect;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private PlayerHitEffect playerHitEffect;

    public override void HitEvent(bool isDeath)
    {
        playerHitEffect.OnPlayerHitEffect();

        SEManager.Instance.Play(SEPath.DAMAGE);

        if (isDeath)
        {
            SEManager.Instance.Play(SEPath.DETH, 1, 0, 1.5f);
            var e = ObjectPool.Instance.CreateEffect(deathEffect);
            e.transform.position = transform.position;
            CameraShaker.Instance.ShakeCamera(1.0f, 30, 1.5f);
        }
        else
        {
            var e = ObjectPool.Instance.CreateEffect(effect);
            e.transform.position = transform.position;
            CameraShaker.Instance.ShakeCamera(0.3f, 30, 1.0f);
        }
    }
}