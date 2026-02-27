using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI Elemanlarý")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    public GameObject gameOverPanel; // Gizlediðin paneli buraya baðlayacaðýz

    private float distanceScore = 0;
    private int totalCoins = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        // Oyun baþýnda panelin kapalý olduðundan emin olalým
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameActive)
        {
            distanceScore += Time.deltaTime * 10f;
            UpdateUI();
        }
    }

    // YENÝ: Oyun bittiðinde GameManager bu fonksiyonu çaðýracak
    public void ShowGameOverUI()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void AddCoin()
    {
        totalCoins += 1;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "Score: " + (int)distanceScore;
        if (coinText != null) coinText.text = "Coins: " + totalCoins;
    }
}