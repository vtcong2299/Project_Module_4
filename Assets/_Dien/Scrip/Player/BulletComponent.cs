using System;
using System.Collections;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    ITarget target;
    ITransformGettable origin;
    Action<GameObject> onReachTarget;
    Vector3 direction;
    [SerializeField]
    float maxDistanceFromOrigin = 30f;
    [SerializeField]
    LayerMask enemyLayer;
    bool canUpdate;
    float damage;

    [SerializeField] float moveSpeed = 20f;

    public void SetDependencies(ITransformGettable player, ITarget bulletTarget, Action<GameObject> action)
    {
        onReachTarget = action;
        target = bulletTarget;
        origin = player;
    }
    private void OnEnable()
    {
        StartCoroutine(SetTheDirection());
    }
    IEnumerator SetTheDirection()
    {
        yield return new WaitUntil(() => target!=null && onReachTarget!=null && origin!=null);
        if (target.someTarget != null)
        {
            direction = (target.someTarget.transform.position - transform.position).normalized;
        }
        canUpdate = true;
    }
    private void Update()
    {
        damage = DataPlayer.Instance.damageMax;
        if (!canUpdate)
        {
            return;
        }
        BulletMove();
        CheckRaycast();
        if (Vector3.Distance(transform.position, origin._transform.position) > maxDistanceFromOrigin)
        {
            onReachTarget.Invoke(gameObject);
        }
    }
    void BulletMove()
    {
        // Di chuyển đạn theo hướng đó
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
    void CheckRaycast()
    {
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, moveSpeed * Time.deltaTime, enemyLayer))
        {
            // Kiểm tra xem đối tượng bị trúng có component SendDamage không
            Enemy targetComponent = hit.transform.GetComponent<Enemy>();
            Debug.Log(targetComponent);
            if (targetComponent != null)
            {
                // Gọi hàm SendDamage trên đối tượng trúng
                targetComponent.TakeDamage(damage);
                LifeSteal();
                onReachTarget.Invoke(gameObject);
            }
        }
    }

    void LifeSteal()
    {
        PlayerCtrl.Instance.playerReceiveDame.HealHp(damage);
    }
}
