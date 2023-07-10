using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AShooter : MonoBehaviour
{
    [SerializeField] protected Bullet bullet;
    [SerializeField] protected Transform muzzle;

    protected bool isShootNow;
    public bool getIsShootNow { get { return isShootNow; } set { isShootNow = value; } }
    public abstract void Shoot(BulletType type);
}