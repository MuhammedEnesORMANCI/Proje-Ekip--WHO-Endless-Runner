using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public static GroundSpawner Instance;
    public GameObject groundPrefab;
    public float tileLength = 3f;

    
    // En son üretilen yolu takip edeceðiz
    private GameObject lastSpawnedTile;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Ýlk yolu tam karakterin altýna (0,0,0) koy
        lastSpawnedTile = Instantiate(groundPrefab, Vector3.zero, Quaternion.identity);

        // Kalan 4 yolu peþ peþe diz
        for (int i = 0; i < 4; i++)
        {
            SpawnTile();
        }
    }

    public void SpawnTile()
    {
        // YOLU OLUÞTURMA (Senin çalýþan sistemin)
        Vector3 spawnPos = lastSpawnedTile.transform.position + new Vector3(0, 0, tileLength);
        lastSpawnedTile = Instantiate(groundPrefab, spawnPos, Quaternion.identity);


    }
}//son hali çalýþýr durumu