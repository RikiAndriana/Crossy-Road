using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject grass;
    [SerializeField] GameObject road;
    [SerializeField] int extent = 7;
    [SerializeField] int frontDistance = 10;
    [SerializeField] int backDistance = -5;
    [SerializeField] int masSameTerrainRepeat = 3;

    Dictionary<int, TerrainBlock> map = new Dictionary<int, TerrainBlock>(50);

    TMP_Text gameOverText;

    private void Start()
    {
        //setup GameOver panel
        gameOverPanel.SetActive(false);
        gameOverText = gameOverPanel.GetComponentInChildren<TMP_Text>();


        //belakang
        for (int z = backDistance; z <= 0; z++)
        {
            CreateTerrain(grass, z);
        }

        //depan
        for (int z = 1; z <= frontDistance; z++)
        {
            var prefab = GetNextTerrainPrefab(z);

            //instantiate blocknya
            CreateTerrain(prefab, z);
        }
        player.SetUp(backDistance, extent);

    }
    private int playerLastMaxTravel;
    private Coroutine gameOverCoroutine;

    private void Update()
    {
        // cek player masih hidup ga?
        if (player.IsDie && gameOverCoroutine == null)
            gameOverCoroutine = StartCoroutine(ShowGameOverPanel());

        // Infinite Terrain system
        if (player.MaxTravel == playerLastMaxTravel)
            return;
        playerLastMaxTravel = player.MaxTravel;

        // bikin ke depan
        var randTBPrefab = GetNextTerrainPrefab(player.MaxTravel + frontDistance);
        CreateTerrain(randTBPrefab, player.MaxTravel + frontDistance);

        // hapus yang di belakang
        var lastTB = map[player.MaxTravel - 1 + backDistance];

        // TerrainBlock lastTB = map[player.MaxTravel + frontDistance];
        // int lastPos = player.MaxTravel;
        // foreach (var (pos, tb) in map)
        // {
        //     if (pos < lastPos)
        //     {
        //         lastPos = pos;
        //         lastTB = tb;
        //     }
        // }

        // hapus dari daftar
        map.Remove(player.MaxTravel - 1 + backDistance);

        // hilangkan dari scene
        // Destroy(lastTB.gameObject);
        if (lastTB is Road)
            PoolSystem.Instance.RoadPool.Release(lastTB.gameObject);
        else
            PoolSystem.Instance.GrassPool.Release(lastTB.gameObject);

        // setup lagi supaya player ga bisa gerak ke belakang
        player.SetUp(player.MaxTravel + backDistance, extent);

    }
    IEnumerator ShowGameOverPanel()
    {
        yield return new WaitForSeconds(3);

        Debug.Log("GameOver");
        gameOverText.text = "YOUR SCORE: " + player.MaxTravel;
        gameOverPanel.SetActive(true);
    }
    private void CreateTerrain(GameObject prefab, int zPos)
    {
        // old code
        // var go = Instantiate(prefab, new Vector3(0, 0, zPos), Quaternion.identity);

        GameObject go;
        if (prefab == road)
            go = PoolSystem.Instance.RoadPool.Get(new Vector3(0, 0, zPos));
        else
            go = PoolSystem.Instance.GrassPool.Get(new Vector3(0, 0, zPos));

        var tb = go.GetComponent<TerrainBlock>();

        if (tb.Extent != extent)
            tb.Build(extent);

        map.Add(zPos, tb);
        // Debug.Log(map[zPos] is Road);
    }
    private GameObject GetNextTerrainPrefab(int nextPos)
    {
        bool isUniform = true;
        var tbRef = map[nextPos - 1];
        for (int distance = 2; distance <= masSameTerrainRepeat; distance++)
        {
            if (map[nextPos - distance].GetType() != tbRef.GetType())
            {
                isUniform = false;
                break;
            }
        }
        if (isUniform)
        {
            if (tbRef is Grass)
                return road;
            else
                return grass;

        }
        //penentuan terrain block dengan probabilitas 50%
        return Random.value > 0.5f ? road : grass;
    }

}
