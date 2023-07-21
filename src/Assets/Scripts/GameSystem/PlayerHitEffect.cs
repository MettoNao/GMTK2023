using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PlayerHitEffect : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPlayerHitEffect()
    {
        canvasGroup.alpha = 1.0f;
        DOTween.To(() => canvasGroup.alpha, (v) => canvasGroup.alpha = v, 0, 0.3f).OnStart(() => canvasGroup.alpha = 1.0f).SetUpdate(true);
    }
}