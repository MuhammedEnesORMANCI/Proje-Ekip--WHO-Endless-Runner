using UnityEngine;

public class GroundTile : MonoBehaviour
{
    private bool spawned = false;

    private void OnTriggerEnter(Collider other)
    {
        // Temas gerçekleþtiðinde Console panelinde yazý çýkacak
        if (!spawned && other.CompareTag("Player"))
        {
            Debug.Log("PLAYER TRIGGER'DAN GEÇTÝ! Yeni yol isteniyor...");
            spawned = true;

            if (GroundSpawner.Instance != null)
            {
                GroundSpawner.Instance.SpawnTile();
            }
        }
    }
}//son hali çalýþýr durumu