using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public RectTransform spawnArea;
    public float spawnIntervalMin = 0.8f;
    public float spawnIntervalMax = 1.5f;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private void Update()
    {
        
    }

    //Starts to spawn coins from objectpool
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            // Wait until game is running
            while (!CoinTapGameManager.Instance.IsGameRunning())
                yield return null;

            yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax));

            // Check again to skip spawning if game was paused during wait
            if (CoinTapGameManager.Instance.IsGameRunning())
                SpawnCoin();
        }
    }

    private void SpawnCoin()
    {
        GameObject coin = CoinPooler.Instance.GetCoin();
        if (coin != null)
        {
            Vector2 spawnPos = GetRandomPositionInSpawnArea();
            coin.transform.SetParent(spawnArea, false);
            coin.GetComponent<RectTransform>().anchoredPosition = spawnPos;
            coin.SetActive(true);
        }
    }

    //To spawn voin within a boundary
    Vector2 GetRandomPositionInSpawnArea()
    {
        Vector2 min = spawnArea.rect.min;
        Vector2 max = spawnArea.rect.max;

        float x = Random.Range(min.x + 50, max.x - 50);
        float y = Random.Range(min.y + 50, max.y - 50);

        return new Vector2(x, y);
    }
}
