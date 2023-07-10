using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;
public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Bullet bullet;

    public void Shoot()
    {
        SEManager.Instance.Play(SEPath.SHOT, 0.1f);
        var b = ObjectPool.Instance.CreateBullet(bullet);
        b.transform.rotation = muzzle.rotation;
        b.Init(GetAngle(transform.position, muzzle.position), moveSpeed, muzzle.position, BulletType.player, 1);
    }

    float GetAngle(Vector2 from, Vector2 to)
    {
        var dx = to.x - from.x;
        var dy = to.y - from.y;
        var rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg;
    }
}