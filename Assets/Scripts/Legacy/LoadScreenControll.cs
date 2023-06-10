using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadScreenControll : MonoBehaviour {

    public GameObject loadingScreenBG;
    public Image progressBar;

    AsyncOperation async;

    public void LoadScreen()
    {
        loadingScreenBG.SetActive(true);
        StartCoroutine(LoadingScreenCur());
    }

    IEnumerator LoadingScreenCur()
    {
        async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;

        while (async.isDone == false)
        {
            progressBar.fillAmount = async.progress;
            if (async.progress == 0.9f)
            {
                progressBar.fillAmount = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
