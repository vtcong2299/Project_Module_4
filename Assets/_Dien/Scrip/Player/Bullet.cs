using UnityEngine;
using DG.Tweening;
using System;

public class Bullet : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] GameObject enemy;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] bool hasTarget = false;
    private float elapsedTime = 0f;
    Action<GameObject> onReachTarget;

    void Update()
    {
        damage = DataPlayer.Instance.damageMax;
        if (!hasTarget)
        {
            if (PlayerCtrl.Instance.playerAttack.closestEnemy == null) return;
            enemy = PlayerCtrl.Instance.playerAttack.closestEnemy;

            hasTarget = true;
        }
        //if (!enemy.activeSelf)
        //{
        //    onReachTarget.Invoke(gameObject);
        //}
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
                LifeSteal();
                onReachTarget.Invoke(gameObject);
            }
        }
    }
    void BulletMove()
    {
        Vector3 direction = (enemy.transform.position - transform.position).normalized;

        // Di chuyển đạn theo hướng đó
        transform.position += direction * moveSpeed * Time.deltaTime;
        elapsedTime += Time.deltaTime;

        // Kiểm tra nếu thời gian đã đạt đến 10 giây
        if (elapsedTime >= 10f)
        {
            onReachTarget.Invoke(gameObject);
            elapsedTime = 0f; // Reset thời gian để tránh gọi lại nhiều lần
        }
        //transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, moveSpeed * Time.deltaTime);
    }

    public void SetOnReachTarget(Action<GameObject> action)
    {
        onReachTarget = action;
    }
    void LifeSteal()
    {
        PlayerCtrl.Instance.playerReceiveDame.HealHp(damage);
    }
}
