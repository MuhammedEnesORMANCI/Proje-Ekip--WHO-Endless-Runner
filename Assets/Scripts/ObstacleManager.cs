using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obstaclePrefab;      // Yerden zýplanacak engel (Kýrmýzý Küp)
    public GameObject highObstaclePrefab;  // Altýndan eðilinerek geçilecek engel
    public float laneDistance = 2f;
    public float spawnInterval = 1.2f;
    public float spawnZPosition = 80f;

    void Start()
    {
        // 2 saniye sonra baþla, spawnInterval süresinde bir tekrarla
        InvokeRepeating("SpawnObstacle", 2f, spawnInterval);
    }

    void SpawnObstacle()
    {
        // Oyun aktif deðilse hiçbir þey yapma
        if (GameManager.Instance != null && !GameManager.Instance.isGameActive) return;

        // Þerit hesaplamasý (Senin sabitlediðin 2f deðeri üzerinden)
        float fixedDistance = 2f;
        float[] lanes = { -fixedDistance, 0, fixedDistance };
        float randomX = lanes[Random.Range(0, lanes.Length)];

        // --- RASTGELE ENGEL SEÇÝMÝ ---
        // %50 ihtimalle yerdeki, %50 ihtimalle havada duran engel seçilir
        GameObject selectedPrefab;
        float yPos;

        if (Random.value > 0.5f)
        {
            selectedPrefab = obstaclePrefab;
            yPos = 1.1f; // Yerdeki engelin yüksekliði (Küpün tam oturmasý için)
        }
        else
        {
            selectedPrefab = highObstaclePrefab;
            yPos = 2f; // Havada duran engel (HighObstacle içindeki küp zaten havada olduðu için 0 idealdir)
        }

        // Seçilen engeli oluþtur
        Vector3 spawnPos = new Vector3(randomX, yPos, spawnZPosition);
        Instantiate(selectedPrefab, spawnPos, Quaternion.identity);

        Debug.Log("Doðan Engel: " + selectedPrefab.name + " | X: " + randomX);
    }
}