using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public GameObject coinPrefab;
    public float spawnInterval = 2.5f;
    public float spawnZPosition = 80f;
    public float laneDistance = 2f;

    void Start()
    {
        InvokeRepeating("SpawnRandomPattern", 3f, spawnInterval);
    }

    void SpawnRandomPattern()
    {
        if (GameManager.Instance == null || !GameManager.Instance.isGameActive || coinPrefab == null) return;

        float[] lanes = { -laneDistance, 0, laneDistance };
        float randomX = lanes[Random.Range(0, lanes.Length)];
        int pattern = Random.Range(0, 3);

        if (pattern == 0) SpawnJumpPattern(randomX);
        else if (pattern == 1) SpawnSlidePattern(randomX);
        else SpawnStraightLine(randomX);
    }

    void SpawnJumpPattern(float x)
    {
        for (int i = 0; i < 5; i++)
        {
            float y = 1.2f + Mathf.Sin((i / 4f) * Mathf.PI) * 1.8f;
            Instantiate(coinPrefab, new Vector3(x, y, spawnZPosition + (i * 2.5f)), Quaternion.identity);
        }
    }

    void SpawnSlidePattern(float x)
    {
        for (int i = 0; i < 4; i++)
            Instantiate(coinPrefab, new Vector3(x, 0.8f, spawnZPosition + (i * 2f)), Quaternion.identity);
    }

    void SpawnStraightLine(float x)
    {
        for (int i = 0; i < 5; i++)
            Instantiate(coinPrefab, new Vector3(x, 1.2f, spawnZPosition + (i * 2f)), Quaternion.identity);
    }
}