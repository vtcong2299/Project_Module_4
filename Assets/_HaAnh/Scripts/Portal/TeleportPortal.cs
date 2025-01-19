using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportPortal : MonoBehaviour
{
    [SerializeField] private string targetScene; // Tên của Scene đích

    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra nếu đối tượng va chạm có tag là "Player"
        if (other.CompareTag("Player"))
        {
            SceneLoader.Instance.SetTriggerFadeIn();
            Invoke("LoadTargetScene", 1);
            SceneLoader.Instance.SetTriggerFadeOut();
        }
    }

    private void LoadTargetScene()
    {
        if (!string.IsNullOrEmpty(targetScene))
        {
            // Chuyển sang Scene mục tiêu
            SceneManager.LoadScene(targetScene);
        }
        else
        {
            Debug.LogWarning("Target scene name is not set!");
        }
    }


}