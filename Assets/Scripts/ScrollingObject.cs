using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    void Update()
    {
        if (GameManager.Instance.isGameActive)
        {
            transform.position += Vector3.back * GameManager.Instance.gameSpeed * Time.deltaTime;
        }

        // Kamera arkasýnda kalan yollarý sil (Sahne temizliði)
        if (transform.position.z < -30f)
        {
            Destroy(gameObject);
        }
    }
}// son hali çalýþýr durumu