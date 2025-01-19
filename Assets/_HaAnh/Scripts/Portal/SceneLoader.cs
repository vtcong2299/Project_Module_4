using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Animator fadeAnimator; // Animator của FadeCanvas
    public string nextSceneName; // Tên scene cần load
    public static SceneLoader Instance;
    private void OnEnable()
    {
        Instance = this;
    }
    private void OnDisable()
    {
        Instance = null;
    }
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