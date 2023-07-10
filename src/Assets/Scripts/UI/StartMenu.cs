using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using KanKikuchi.AudioManager;
public class StartMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup.alpha = 1.0f;
        DOTween.To(() => canvasGroup.alpha, (a) => canvasGroup.alpha = a, 0.0f, 0.3f);
    }

    public void OnStart()
    {
        SEManager.Instance.Play(SEPath.BUTTON);
        DOTween.To(() => canvasGroup.alpha, (a) => canvasGroup.alpha = a, 0.0f, 0.3f).SetDelay(0.5f).OnComplete(() => SceneManager.LoadScene("Loading"));
    }

    public void OnOption()
    {
        DOTween.To(() => canvasGroup.alpha, (a) => canvasGroup.alpha = a, 0.0f, 0.3f).SetDelay(0.5f).OnComplete(() => SceneManager.LoadScene("Loading"));
    }

    public void OnPointEnter()
    {
        SEManager.Instance.Play(SEPath.MAP_DOWN);
    }

    public void OnController()
    {
        DOTween.To(() => canvasGroup.alpha, (a) => canvasGroup.alpha = a, 0.0f, 0.3f).SetDelay(0.5f).OnComplete(() => SceneManager.LoadScene("Loading"));
    }

}