using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSystem : MonoBehaviour
{
    private static PoolSystem instance;
    public static PoolSystem Instance { get => instance; }
    public Pool RoadPool { get => roadPool; }
    public Pool GrassPool { get => grassPool; }
    public Pool CarPool { get => carPool; }
    public Pool TreePool { get => treePool; }

    [SerializeField] GameObject roadPrefab;
    [SerializeField] GameObject grassPreafab;
    [SerializeField] GameObject carPrefab;
    [SerializeField] GameObject treePrefab;
    Pool roadPool;
    Pool grassPool;
    Pool carPool;
    Pool treePool;

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);

        instance = this;

        roadPool = new Pool(roadPrefab);
        grassPool = new Pool(grassPreafab);
        carPool = new Pool(carPrefab);
        treePool = new Pool(treePrefab);
    }

}
