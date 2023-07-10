using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraShaker : SingletonMonoBehaviour<CameraShaker>
{
    Vector3 startPos;
    private void Start()
    {
        startPos = transform.position;
    }
    public void ShakeCamera(float time, int count, float scale)
    {
        transform.DOShakePosition(time, scale, count, 1, false)
        .OnComplete(() => transform.position = startPos)
        .OnStart(() => transform.position = startPos)
        .OnUpdate(() => transform.position = new Vector3(transform.position.x, transform.position.y, startPos.z));
    }
}