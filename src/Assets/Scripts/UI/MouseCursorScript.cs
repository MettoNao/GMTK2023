using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    public void setMouseCursorPosition(Vector2 pos)
    {
        transform.position = pos;
    }
}
