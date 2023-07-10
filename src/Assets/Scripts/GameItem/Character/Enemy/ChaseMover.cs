using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseMover : AMover
{
    [SerializeField] private float minDis, maxDis;
    private float dis;
    private Rigidbody2D rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        dis = Random.Range(minDis, maxDis);
    }

    public override void Move(Vector2 target)
    {
        float distace = ((Vector2)transform.position - target).sqrMagnitude;
        rig.velocity = distace < dis * dis ? Vector2.zero : transform.up * moveSpeed;
    }

    public override void Direction(Vector2 target)
    {
        Vector2 direction = target - (Vector2)transform.position;
        var lookRotation = Quaternion.FromToRotation(Vector2.up, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);
    }

    public override void Stop()
    {
        rig.velocity = Vector2.zero;
    }
}
