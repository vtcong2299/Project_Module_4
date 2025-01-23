using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IOnGameStart<IRespawnable>, IOnGameStart<ISpawnable>
{
    [SerializeField] Transform player; // Vị trí player, tâm bám kính tìm kiếm enemy
    [SerializeField] float detectionRadius = 15f; // Bán kính tìm kiếm
    [SerializeField] float delayTime = 2f; // Khoản cách giữa các lầm tìm kiếm enemy mới
    public GameObject closestEnemy;
    [SerializeField] GameObject virtualEnemy;

    [SerializeField] GameObject targerRing;
    PlayerAnim playerAnim;
    float timeElapsed = 0f;
    bool canAttack;

    IRespawnable respawner;
    Transform checkPoint;
    ISpawnable bulletSpawner;

    Action<IRespawnable> IOnGameStart<IRespawnable>.onGameStartAction => spawner => respawner = spawner;

    Action<ISpawnable> IOnGameStart<ISpawnable>.onGameStartAction => spawner => bulletSpawner = spawner;

    private void Start()
    {
        playerAnim = GetComponent<PlayerAnim>();
        StartCoroutine(FindClosestEnemyCoroutine());
    }
    void Update()
    {
        TargetRing();
        CheckDistanceAndRespawn();
    }
    void FixedUpdate()
    {
        ToAttack();
    }

    IEnumerator FindClosestEnemyCoroutine()
    {
        while (true)
        {
                closestEnemy = FindClosestEnemy();
            yield return new WaitForSeconds(delayTime);
        }
    }
    public GameObject FindClosestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(player.position, detectionRadius);
        GameObject closest = null;
        float shortestDistance = Mathf.Infinity;
        if (colliders.Length == 0)
        {
            closest = virtualEnemy;
        }
        else
        {
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    float distance = Vector3.Distance(player.position, collider.transform.position);
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        closest = collider.gameObject;
                        checkPoint = closest.transform;
                    }
                }
            }
        }
        return closest;
    }
    void TargetRing()
    {
        if (closestEnemy == null)
        {
            targerRing.transform.position = new Vector3 (targerRing.transform.position.x, -5f, targerRing.transform.position.z);
        }
        else
        {
            targerRing.transform.position = closestEnemy.transform.position;
        }
    }

    void CheckDistanceAndRespawn()
    {
        if (checkPoint == null)
        {
            return;
        }

        if (Vector3.Distance(transform.position, checkPoint.position) >= respawner.respawnDistance)
        {
            checkPoint = virtualEnemy.transform;
            respawner.Respawn();
        }
    }

    void ToAttack()
    {
        if (closestEnemy == null)
        {
            return;
        }
        timeElapsed += Time.fixedDeltaTime;
        if (timeElapsed >= DataPlayer.Instance.currentAttackCountdown)
        {
            timeElapsed = 0;
            canAttack = true;
        }
        if (canAttack && closestEnemy != null && playerAnim.IsIdling())
        {
            playerAnim.TriggerAttack();
            bulletSpawner.ToSpawn();
            canAttack = false;
        }
    }
}
