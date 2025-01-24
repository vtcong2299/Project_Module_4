using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelLoading : MonoBehaviour
{
    Image fadeBG;
    string sceneName;

    private void Awake()
    {
        fadeBG = GetComponent<Image>();
    }

    private void OnEnable()
    {
        StartCoroutine(LoadSceneAsyncWithFade());
    }

    public void SetSceneName(string sceneName)
    {
        this.sceneName = sceneName;
    }

    IEnumerator LoadSceneAsyncWithFade()
    {
        yield return new WaitUntil(() => sceneName != null);
        StartCoroutine(LoadSceneWithProgress(sceneName));
    }

    IEnumerator LoadSceneWithProgress(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            fadeBG.color = new Color(0, 0, 0, progress);

            if (asyncOperation.progress >= 0.9f)
            {
                DOTween.To(() => fadeBG.color, x => fadeBG.color = x, new Color(0.75f, 0.75f, 0.75f, 1), 2f).SetUpdate(true).OnComplete(() =>
                {
                    asyncOperation.allowSceneActivation = true;
                });
                break;
            }
            yield return null;
        }
        Time.timeScale = 1;
    }
}
