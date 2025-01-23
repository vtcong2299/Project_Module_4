using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IOnGameStart<IRespawnable>, IOnGameStart<ISpawnable>, IOnEnemyDie
{
    [SerializeField] float detectionRadius = 15f; // Bán kính tìm kiếm
    [SerializeField] float delayTime = 2f; // Khoản cách giữa các lầm tìm kiếm enemy mới
    [SerializeField] LayerMask enemyMask;
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
            if (closestEnemy != null)
            {
                targerRing.transform.position = new Vector3 (closestEnemy.transform.position.x, 0.1f, closestEnemy.transform.position.z);
                targerRing.transform.SetParent(closestEnemy.transform);
            }
            else
            {
                targerRing.transform.position = new Vector3(targerRing.transform.position.x, -5f, targerRing.transform.position.z);
            }
            yield return new WaitForSeconds(delayTime);
        }
    }
    public GameObject FindClosestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyMask);
        GameObject closest = null;
        float shortestDistance = Mathf.Infinity;

        if (colliders.Length == 0)
        {
            return closest;
        }
        else
        {
            foreach (Collider collider in colliders)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closest = collider.gameObject;
                    checkPoint = closest.transform;
                }
            }
        }
        return closest;
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

    public void OnEnemyDie(GameObject dyingEnemy, float exp)
    {
        if (closestEnemy == dyingEnemy)
        {
            targerRing.transform.position = new Vector3(targerRing.transform.position.x, -5f, targerRing.transform.position.z);
            closestEnemy = null;
        }
    }
}
