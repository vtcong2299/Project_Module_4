using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour, ISpawnable
{
    public Transform spawnOrigin;
    public GameObject prefab;
    public float spawnDelay = 0.05f;
    public List<GameObject> objPool;
    public List<GameObject> activeObjs;

    private void Awake()
    {
        objPool = new List<GameObject>();
        activeObjs = new List<GameObject>();
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnDelay);
        if (objPool.Count > 0)
        {
            TakeFromPool(spawnOrigin.position);
        }
        else
        {
            GameObject obj = Instantiate(prefab, spawnOrigin.position, Quaternion.identity, transform);
            obj.GetComponent<Bullet>().SetOnReachTarget(pushedObj => PushToPool(pushedObj));
            activeObjs.Add(obj);
        }
    }

    private void TakeFromPool(Vector3 spawnPosition)
    {
        objPool[objPool.Count - 1].SetActive(true);
        objPool[objPool.Count - 1].transform.position = spawnPosition;
        activeObjs.Add(objPool[objPool.Count - 1]);
        objPool.RemoveAt(objPool.Count - 1);
    }

    void PushToPool(GameObject obj)
    {
        foreach (GameObject item in activeObjs)
        {
            if (item == obj)
            {
                objPool.Add(item);
                activeObjs.Remove(item);
                item.SetActive(false);
                break;
            }
        }
    }

    public void ToSpawn()
    {
        StartCoroutine(Spawn());
    }
}
