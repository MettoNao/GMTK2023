using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
    [SerializeField] private float lookSpeed;

    public void Direction(Vector2 dir, bool isGamePad)
    {
        var pos = isGamePad ? Vector2.zero : (Vector2)Camera.main.WorldToScreenPoint(transform.position);
        var rotation = Quaternion.LookRotation(Vector3.forward, dir - pos);
        transform.rotation = rotation;
    }
}