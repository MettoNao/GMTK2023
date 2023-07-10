using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class LoadingScript : MonoBehaviour
{
    private AsyncOperation _async;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float Min_Load_Time;
    [SerializeField] private Image slider;

    private void Start()
    {
        StartCoroutine(LoadData());
    }


    IEnumerator LoadData()
    {
        yield return new WaitForSeconds(1.0f);

        _async = SceneManager.LoadSceneAsync("Main");

        _async.allowSceneActivation = false;

        while (_async.progress < 0.9f)
        {
            slider.fillAmount = _async.progress;

            yield return new WaitForSeconds(0.1f);
        }

        slider.fillAmount = 1.0f;
        _async.allowSceneActivation = true;

        yield return _async;
    }
}
