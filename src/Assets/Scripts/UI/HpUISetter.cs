using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class HpUISetter : MonoBehaviour
{
    private Image hpFill;
    private Tween _tween = null;
    // Start is called before the first frame update
    void Awake()
    {
        hpFill = GetComponent<Image>();
    }

    public void NoAnimationSetHpFill(float hp, float max)
    {
        hpFill.fillAmount = hp / max;
    }

    public void SetHpFill(float hp, float max)
    {
        if (_tween != null)
        {
            _tween.Kill(false);
            _tween = null;
        }
        _tween = DOTween.To(() => hpFill.fillAmount, (v) => hpFill.fillAmount = v, hp / max, 0.2f).SetLink(gameObject);
    }

    private void OnDisable()
    {
        if (DOTween.instance != null)
        {
            _tween?.Kill();
        }
    }

}
