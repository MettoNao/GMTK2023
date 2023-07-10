using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class ExclamationScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup img;
    private bool isExclamation = false;

    public void ShowExclamation()
    {
        isExclamation = true;
        img.transform.localScale = new Vector3(1, 0, 1);
        img.alpha = 1.0f;
        img.transform.DOScaleY(1.2f, 0.3f).SetEase(Ease.OutBounce).OnComplete(() => DOTween.To(() => img.alpha, (v) => img.alpha = v, 0, 0.3f).SetDelay(1.0f).OnComplete(() => isExclamation = false));
    }

    private void Update()
    {
        if (isExclamation)
        {
            transform.rotation = Quaternion.identity;
        }
    }
}