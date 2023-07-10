using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;
public class PlayerSpecialShooter : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Bullet bullet;

    [SerializeField] private AlertUIEffect alertUIEffect;
    [SerializeField] private HpUISetter hpUISetter;

    private int skillPoint;
    private int maxSkillPoint = 8;

    public void Shoot()
    {
        if (skillPoint < maxSkillPoint)
        {
            alertUIEffect.onAlert();
            return;
        }

        SEManager.Instance.Play(SEPath.SHOT, 0.1f);

        skillPoint = 0;

        hpUISetter.SetHpFill(skillPoint, maxSkillPoint);

        var b = ObjectPool.Instance.CreateBullet(bullet);
        b.transform.rotation = muzzle.rotation;
        b.Init(GetAngle(transform.position, muzzle.position), moveSpeed, muzzle.position, BulletType.playerAlly, 0);
    }

    float GetAngle(Vector2 from, Vector2 to)
    {
        var dx = to.x - from.x;
        var dy = to.y - from.y;
        var rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("skillItem"))
        {
            SEManager.Instance.Play(SEPath.COIN);
            skillPoint++;
            hpUISetter.SetHpFill(skillPoint, maxSkillPoint);
            other.gameObject.SetActive(false);
        }
    }
}