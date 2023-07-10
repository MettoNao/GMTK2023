using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using KanKikuchi.AudioManager;
public class GameOverMenuScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup, fade;
    [SerializeField] private TextMeshProUGUI scoreText;

    public void InitMenu(int score)
    {
        Cursor.visible = true;
        scoreText.text = "SCORE:" + score.ToString();
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        DOTween.To(() => canvasGroup.alpha, (v) => canvasGroup.alpha = v, 1.0f, 0.5f).SetDelay(2.0f);
    }

    private bool isClickBackStartMenu = false;
    public void BackStartMenu()
    {
        if (isClickBackStartMenu) return;
        SEManager.Instance.Play(SEPath.BUTTON);
        isClickBackStartMenu = true;
        DOTween.To(() => fade.alpha, (v) => fade.alpha = v, 1.0f, 0.5f).OnComplete(() => SceneManager.LoadScene("Start"));
    }

    private bool isClickPlayAgain = false;
    public void PlayAgain()
    {
        if (isClickPlayAgain) return;
        SEManager.Instance.Play(SEPath.BUTTON);
        isClickPlayAgain = true;
        DOTween.To(() => fade.alpha, (v) => fade.alpha = v, 1.0f, 0.5f).OnComplete(() => SceneManager.LoadScene("Loading"));
    }
}
