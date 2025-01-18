using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyManager : MonoBehaviour
{
    private void Awake()
    {
        // Đảm bảo đối tượng không bị phá hủy khi chuyển scene
        DontDestroyOnLoad(gameObject);
    }
}
