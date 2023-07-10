using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeHpUIColor : MonoBehaviour
{
    [SerializeField] private Image backGround, hpFill;

    public void SetColor(bool isAlly)
    {
        backGround.color = isAlly ? new Color(0, 0.3f, 0) : new Color(0.3f, 0, 0);
        hpFill.color = isAlly ? new Color(0.3f, 1.0f, 0.3f) : new Color(1.0f, 0.3f, 0.3f);
    }
}
