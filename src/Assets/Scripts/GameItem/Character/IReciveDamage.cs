using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReciveDamage
{
    public void ReciveDamage(int attackPoint, BulletType type);
}