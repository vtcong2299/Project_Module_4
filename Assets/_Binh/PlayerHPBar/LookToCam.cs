using UnityEngine;

public class LookToCam : MonoBehaviour
{
    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position);
    }
}
