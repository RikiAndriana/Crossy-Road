using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField] GameObject treePrefab;
    [SerializeField] TerrainBlock terrain;
    [SerializeField] int minCount = 3;
    [SerializeField] int maxCount = 7;

    List<GameObject> treeList = new List<GameObject>();

    private void OnEnable()
    {
        StartCoroutine(SpawnTrees());
    }
    IEnumerator SpawnTrees()
    {
        if (terrain.Extent == 0)
            yield return new WaitUntil(() => terrain.Extent > 0);

        GameObject go;
        List<Vector3> emptyPos = new List<Vector3>();
        treeList.Clear();

        for (int x = -terrain.Extent; x <= terrain.Extent; x++)
        {
            if (transform.position.z == 0 && x == 0)
                continue;

            emptyPos.Add(transform.position + Vector3.right * x);
        }

        var count = Random.Range(minCount, maxCount + 1);
        for (int i = 0; i < count; i++)
        {
            var index = Random.Range(0, emptyPos.Count);
            var spawnPos = emptyPos[index];
            // Instantiate(treePrefab, spawnPos, Quaternion.identity, this.transform);
            // emptyPos.RemoveAt(index);
            go = PoolSystem.Instance.TreePool.Get(spawnPos);
            go.transform.parent = this.transform;

            treeList.Add(go);

            emptyPos.RemoveAt(index);
        }
        // Instantiate(
        //     treePrefab,
        //     transform.position + Vector3.right * -(terrain.Extent + 1),
        //     Quaternion.identity,
        //     this.transform);
        // Instantiate(
        //     treePrefab,
        //     transform.position + Vector3.right * (terrain.Extent + 1),
        //     Quaternion.identity,
        //     this.transform);

        go = PoolSystem.Instance.TreePool.Get(
            transform.position + Vector3.right * -(terrain.Extent + 1)
        );
        go.transform.parent = this.transform;
        treeList.Add(go);

        go = PoolSystem.Instance.TreePool.Get(
            transform.position + Vector3.right * (terrain.Extent + 1)
        );
        go.transform.parent = this.transform;
        treeList.Add(go);
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        foreach (var item in treeList)
        {
            PoolSystem.Instance.TreePool.Release(item);
        }
        treeList.Clear();
    }
}
