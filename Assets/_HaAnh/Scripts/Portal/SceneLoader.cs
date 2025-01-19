using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : Singleton<SceneLoader>
{
    public Animator fadeAnimator; // Animator của FadeCanvas
    // Hàm gọi khi bắt đầu chuyển scene
   public void SetTriggerFadeIn()
    {
        fadeAnimator.SetTrigger("FadeIn");
    }
    public void SetTriggerFadeOut()
    {
        fadeAnimator.SetTrigger("FadeOut");
    }
        // Load scene mới
        //SceneManager.LoadScene(nextSceneName);

    

}