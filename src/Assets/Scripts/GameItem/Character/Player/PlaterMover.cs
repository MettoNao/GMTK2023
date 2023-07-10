using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaterMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rig;

    private ClampScript clamp;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        clamp = new ClampScript();
    }

    public void Move(Vector2 dir)
    {
        rig.velocity = dir.normalized * moveSpeed;
        transform.position = clamp.Clamp(transform.position);
    }
}