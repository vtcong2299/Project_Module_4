using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;

    void Update()
    {
        CheckRaycast();
    }
    void CheckRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            // Kiểm tra xem đối tượng bị trúng có component SendDamage không
            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null)
            {
                // Gọi hàm SendDamage trên đối tượng trúng
                target.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
