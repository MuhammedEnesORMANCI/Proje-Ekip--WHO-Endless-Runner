using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isGameActive = false;
    public float gameSpeed = 10f;
    public float speedIncreaseRate = 0.1f;
    public float maxSpeed = 35f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Time.timeScale = 1;
    }

    void Update()
    {
        // Baþlatma ve Yeniden Baþlatma Kontrolü
        if (!isGameActive && Input.GetKeyDown(KeyCode.Space))
        {
            // Eðer oyun hiç baþlamadýysa baþlat, bittiyse restart at
            // Not: score kontrolünü ScoreManager üzerinden de yapabiliriz ama 
            // þimdilik basitçe isGameActive üzerinden yönetmek yeterli.
            if (Time.timeSinceLevelLoad < 0.5f) // Sahne yeni yüklendiyse
            {
                StartGame();
            }
            else 
            {
                RestartGame();
            }
        }

        if (!isGameActive) return;

        // Hýzlanma mantýðý burada kalmalý (Çünkü bu oyunun beyniyle ilgili)
        if (gameSpeed < maxSpeed)
        {
            gameSpeed += speedIncreaseRate * Time.deltaTime;
        }
    }

    public void StartGame()
    {
        isGameActive = true;
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        isGameActive = false;
        Time.timeScale = 0; 
        
        // UI'ý açmasý için ScoreManager'a haber veriyoruz
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ShowGameOverUI();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}