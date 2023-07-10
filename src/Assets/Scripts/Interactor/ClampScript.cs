using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampScript
{
    public bool isInScreen(Vector2 pos)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        Vector2 clampPos = pos;

        return pos.x < min.x || pos.x > max.x || pos.y < min.y || pos.y > max.y;
    }

    public Vector2 Clamp(Vector2 pos)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        Vector2 clampPos = pos;

        clampPos.x = Mathf.Clamp(clampPos.x, min.x, max.x);
        clampPos.y = Mathf.Clamp(clampPos.y, min.y, max.y);

        return clampPos;
    }
}