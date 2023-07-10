using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;
public class NormalShooter : AShooter
{
    [SerializeField] private int attackPoint;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float angle;
    [SerializeField] private int bulletCount;
    [SerializeField] private float shotTime;
    [SerializeField] private float shotInterval;

    public override void Shoot(BulletType type)
    {
        isShootNow = true;

        SEManager.Instance.Play(SEPath.SHOT, 0.1f);

        if (1 < bulletCount)
        {
            for (int i = 0; i < bulletCount; ++i)
            {
                float a = GetAngle(transform.position, muzzle.position) + angle * ((float)i / (bulletCount - 1) - 0.5f);

                var b = ObjectPool.Instance.CreateBullet(bullet);

                b.transform.position = muzzle.position;
                b.transform.rotation = muzzle.rotation;

                b.Init(a, bulletSpeed, muzzle.position, type, attackPoint);
            }
        }
        else if (bulletCount == 1)
        {
            var b = ObjectPool.Instance.CreateBullet(bullet);

            b.transform.position = muzzle.position;
            b.transform.rotation = muzzle.rotation;

            b.Init(GetAngle(transform.position, muzzle.position), bulletSpeed, muzzle.position, type, attackPoint);
        }

        StartCoroutine(SetIsShootNow());
    }

    IEnumerator SetIsShootNow()
    {
        yield return new WaitForSeconds(shotTime);
        isShootNow = false;
    }

    void OnDisable()
    {
        StopCoroutine(SetIsShootNow());
        isShootNow = false;
    }

    float GetAngle(Vector2 from, Vector2 to)
    {
        var dx = to.x - from.x;
        var dy = to.y - from.y;
        var rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg;
    }

}