using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FadeScript : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        FdaeIn();
    }

    public void FdaeIn()
    {
        canvasGroup.alpha = 1;
        DOTween.To(() => canvasGroup.alpha, (v) => canvasGroup.alpha = v, 0, 0.3f);
    }

    public void FdaeOut()
    {
        canvasGroup.alpha = 0;
        DOTween.To(() => canvasGroup.alpha, (v) => canvasGroup.alpha = v, 1.0f, 0.3f);
    }
}
