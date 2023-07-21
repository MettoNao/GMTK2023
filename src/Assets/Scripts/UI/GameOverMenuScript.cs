using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using KanKikuchi.AudioManager;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class GameOverMenuScript : MonoBehaviour
{
    [SerializeField] Button firstControls;
    [SerializeField] private CanvasGroup canvasGroup, fade;
    [SerializeField] private TextMeshProUGUI scoreText, scoreText2;

    public void InitMenu(int score)
    {

        if (Gamepad.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            firstControls.Select();
        }

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
