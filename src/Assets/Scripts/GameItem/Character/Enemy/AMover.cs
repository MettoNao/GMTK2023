using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AMover : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    public abstract void Move(Vector2 taregt);
    public abstract void Direction(Vector2 taregt);
    public abstract void Stop();
}