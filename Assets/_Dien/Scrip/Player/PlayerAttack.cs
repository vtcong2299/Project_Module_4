using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IOnGameStart<IRespawnable>
{
    [SerializeField] Transform player; // Vị trí player, tâm bám kính tìm kiếm enemy
    [SerializeField] float detectionRadius = 25f; // Bán kính tìm kiếm
    [SerializeField] float delayTime = 2f; // Khoản cách giữa các lầm tìm kiếm enemy mới
    [SerializeField] GameObject closetEnemy;
    [SerializeField] GameObject virtualEnemy;

    [SerializeField] GameObject targerRing;

    IRespawnable respawner;
    Transform checkPoint;

    public Action<IRespawnable> onGameStartAction => spawner => respawner = spawner;

    private void Start()
    {
        StartCoroutine(FindClosestEnemyCoroutine());
    }
    void Update()
    {
        TargetRing();
        CheckDistanceAndRespawn();
    }
    IEnumerator FindClosestEnemyCoroutine()
    {
        while (true)
        {
            closetEnemy = FindClosestEnemy();
            yield return new WaitForSeconds(delayTime);
        }
    }
    GameObject FindClosestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(player.position, detectionRadius);
        GameObject closet = null;
        float shortesDistance = Mathf.Infinity;

        
        if (colliders.Length == 0)
        {
            closet = virtualEnemy;
        }
        else
        {
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    float distance = Vector3.Distance(player.position, collider.transform.position);
                    if (distance < shortesDistance)
                    {
                        shortesDistance = distance;
                        closet = collider.gameObject;
                        checkPoint = closet.transform;
                    }
                }
            }
        }
        
        return closet;
    }
    void TargetRing()
    {
        if (closetEnemy == null)
        {
            targerRing.transform.position = new Vector3 (targerRing.transform.position.x, -5f, targerRing.transform.position.z);
        }
        else
        {
            targerRing.transform.position = closetEnemy.transform.position;
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
}
