using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Þerit Ayarlarý")]
    public float laneDistance = 2f;
    public float moveSpeed = 15f;
    private int targetLane = 1;

    [Header("Zýplama & Fizik")]
    public float jumpForce = 10f;
    public float gravityMultiplier = 10f;
    public float groundCheckDistance = 0.2f;

    [Header("Eðilme Ayarlarý")]
    public float slideDuration = 1f;
    private bool isSliding = false;
    private Vector3 originalScale;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        originalScale = transform.localScale;
        rb = GetComponent<Rigidbody>();
        // Z ekseninde kaymayý ve dönmeyi engelle
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }

    void Update()
    {
        if (!GameManager.Instance.isGameActive) return;

        // Yer Kontrolü
        isGrounded = Physics.Raycast(transform.position, Vector3.down, (GetComponent<Collider>().bounds.extents.y) + groundCheckDistance);

        // Þerit Deðiþtirme
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) if (targetLane > 0) targetLane--;
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) if (targetLane < 2) targetLane++;

        float targetX = (targetLane - 1) * laneDistance;
        Vector3 targetPos = new Vector3(targetX, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);

        // Zýplama
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Yerçekimi Hissi
        if (rb.velocity.y < 0) rb.velocity += Vector3.up * Physics.gravity.y * (gravityMultiplier - 1) * Time.deltaTime;

        // Eðilme
        // Zýplama ve süzülme kontrolü içinde þurayý bul veya ekle:
        // --- S TUÞU (AÞAÐI) KONTROLÜ ---
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (isGrounded && !isSliding)
            {
                // Yerdeysek: Eðil (Slide)
                StartCoroutine(Slide());
            }
            else if (!isGrounded)
            {
                // Havadaysak: Yere Çakýl (Slam)
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Mevcut yukarý hýzý sýfýrla
                rb.AddForce(Vector3.down * jumpForce * 3f, ForceMode.Impulse); // Daha sert bir iniþ
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver(); // Engel çarpýþmasý (Bozulmadý)
        }
        else if (other.CompareTag("Coin"))
        {
            // Eskiden: GameManager.Instance.score += 50;
            // Yeni: Skor yönetimini ScoreManager'a devrediyoruz
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddCoin();
            }

            Destroy(other.gameObject); // Altýný yok etme (Bozulmadý)
        }
    }

    System.Collections.IEnumerator Slide()
    {
        isSliding = true;
        transform.localScale = new Vector3(originalScale.x, originalScale.y * 0.5f, originalScale.z);
        yield return new WaitForSeconds(slideDuration);
        transform.localScale = originalScale;
        isSliding = false;
    }
}