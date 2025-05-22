using System.Collections.Generic;
using UnityEngine;

public class CoinPooler : MonoBehaviour
{
    public static CoinPooler Instance;

    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int poolSize = 20;

    private List<GameObject> coinPool = new List<GameObject>();

    private void Awake()
    {
        // Pooling Coin Prefab for Optimization
        Instance = this;
        for (int i = 0; i < poolSize; i++)
        {
            GameObject coin = Instantiate(coinPrefab, transform);
            coin.SetActive(false);
            coinPool.Add(coin);
        }
    }

    //Used to spawns coin
    public GameObject GetCoin()
    {
        foreach (var coin in coinPool)
        {
            if (!coin.activeInHierarchy)
                return coin;
        }
        return null;
    }

    //Used to deactivate coin after click
    public void ReturnCoin(GameObject coin)
    {
        coin.SetActive(false);
    }
}
