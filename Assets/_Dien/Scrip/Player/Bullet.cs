using UnityEngine;
using DG.Tweening;
using System;

public class Bullet : MonoBehaviour
{
    [SerializeField] float damage = 10f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] GameObject enemy;
    [SerializeField] LayerMask enemyLayer;

    Action<GameObject> onReachTarget;

    void Update()
    {
        if (PlayerCtrl.Instance.playerAttack.closestEnemy == null) return;
        enemy = PlayerCtrl.Instance.playerAttack.closestEnemy;
        BulletMove();
        CheckRaycast();
    }
    void CheckRaycast()
    {
        if (enemy == null) return;

        Vector3 direction = (enemy.transform.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, moveSpeed * Time.deltaTime, enemyLayer))
        {
            // Kiểm tra xem đối tượng bị trúng có component SendDamage không
            Enemy target = hit.transform.GetComponent<Enemy>();
            Debug.Log(target);
            if (target != null)
            {
                // Gọi hàm SendDamage trên đối tượng trúng
                target.TakeDamage(damage);
                onReachTarget.Invoke(gameObject);
            }
        }
    }
    void BulletMove()
    {
        Vector3 direction = (enemy.transform.position - transform.position).normalized;

        // Di chuyển đạn theo hướng đó
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    public void SetOnReachTarget(Action<GameObject> action)
    {
        onReachTarget = action;
    }
}
