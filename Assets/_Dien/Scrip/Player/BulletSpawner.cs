using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public Transform spawnOrigin;
    public List<GameObject> objPool;
    public List<GameObject> activeObjs;
    public GameObject preFab;
    public float spawnDelay = 1f;
    public int maxObj = 2;
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
            GameObject obj = Instantiate(preFab, spawnOrigin.position, Quaternion.identity, transform);
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
}
