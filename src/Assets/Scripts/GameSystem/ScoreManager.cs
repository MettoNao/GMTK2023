using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText, comboText;
    [SerializeField] private CanvasGroup canvasGroup;

    int combo;
    int score;
    public void AddScore(int add)
    {
        int old = score;
        int updateScore = score;
        score += add * (combo + 1);

        DOTween.To(() => old, (s) => updateScore = s, score, 0.3f).OnUpdate(() => scoreText.text = updateScore.ToString().PadLeft(6, '0'));
    }

    public void AddCombo()
    {
        timer = interval;
        combo++;
        if (combo <= 1) return;

        comboText.text = "x" + combo.ToString();
        comboText.transform.localScale = Vector3.zero;
        comboText.transform.DOScale(1.2f, 0.2f).OnComplete(() => comboText.transform.DOScale(1.0f, 0.1f));
        DOTween.To(() => canvasGroup.alpha, (a) => canvasGroup.alpha = a, 1.0f, 0.2f).OnComplete(() => DOTween.To(() => canvasGroup.alpha, (a) => canvasGroup.alpha = a, 0.0f, 0.2f).SetDelay(1.7f));
    }

    float timer = 3.0f;
    float interval = 3.0f;
    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            combo = 0;
            timer = interval;
        }
    }

    public int GetScore()
    {
        return score;
    }
}