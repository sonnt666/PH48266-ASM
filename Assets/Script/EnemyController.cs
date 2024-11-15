using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;           // Tốc độ di chuyển của quái
    public float changeDirectionTime = 2f; // Thời gian đổi hướng
    public CircleCollider2D movementBoundary; // Tham chiếu đến vùng di chuyển

    private Vector2 movement;
    private float timer;
    private SpriteRenderer spriteRenderer;
    private Vector2 boundaryCenter;

    void Start()
    {
        // Gán SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Xác định vị trí tâm của vùng di chuyển dựa trên CircleCollider2D
        if (movementBoundary != null)
        {
            boundaryCenter = movementBoundary.transform.position;
        }
        else
        {
            Debug.LogWarning("Chưa gán vùng di chuyển cho quái!");
        }

        timer = changeDirectionTime;
        ChooseNewDirection();
    }

    void Update()
    {
        // Đếm thời gian để thay đổi hướng
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            ChooseNewDirection();
            timer = changeDirectionTime;
        }

        // Di chuyển quái
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // Kiểm tra và lật mặt quái dựa trên hướng di chuyển
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false; // Hướng mặt sang phải
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true;  // Hướng mặt sang trái
        }

        // Kiểm tra nếu quái ra ngoài vùng di chuyển
        if (Vector2.Distance(boundaryCenter, transform.position) > movementBoundary.radius)
        {
            // Nếu ra ngoài ranh giới, quay lại bên trong vùng và chọn hướng mới
            Vector2 directionToCenter = (boundaryCenter - (Vector2)transform.position).normalized;
            movement = directionToCenter;
            timer = changeDirectionTime; // Reset lại bộ đếm để tránh đổi hướng quá sớm
        }
    }

    // Hàm chọn hướng di chuyển ngẫu nhiên
    void ChooseNewDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        movement = new Vector2(randomX, randomY).normalized;
    }
}
