using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AlertUIEffect : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void onAlert()
    {
        canvasGroup.alpha = 1.0f;
        DOTween.To(() => canvasGroup.alpha, (a) => canvasGroup.alpha = a, 0.0f, 0.3f);
    }
}