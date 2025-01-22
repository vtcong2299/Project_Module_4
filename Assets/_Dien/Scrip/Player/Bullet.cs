using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    [SerializeField] float damage = 10f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] GameObject enemy;

    void Update()
    {
        if (PlayerCtrl.Instance.playerAttack.closestEnemy == null) return;
        enemy = PlayerCtrl.Instance.playerAttack.closestEnemy;
        BulletMove();
        CheckRaycast();
    }
    void CheckRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1))
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
    void BulletMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, moveSpeed * Time.deltaTime);
    }
}
