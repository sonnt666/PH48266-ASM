using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    private float moveSpeed;           // Tốc độ di chuyển của quái
    public float changeDirectionTime = 2f; // Thời gian đổi hướng
    public float chaseDistance = 5f;       // Khoảng cách để bắt đầu đuổi theo
    public Transform player;               // Tham chiếu đến nhân vật
    private Vector2 movement;
    private float timer;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Gán SpriteRenderer và đặt thời gian để chọn hướng ngẫu nhiên ban đầu
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = changeDirectionTime;
        ChooseNewDirection();
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= chaseDistance)
            {
                ChasePlayer();
            }
            else
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    ChooseNewDirection();
                    timer = changeDirectionTime;
                }
            }
        }
        else
        {
            // Nếu `player` bị hủy, thực hiện hành động khác, ví dụ: dừng quái hoặc chọn hướng ngẫu nhiên
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                ChooseNewDirection();
                timer = changeDirectionTime;
            }
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
    }

    // Hàm chọn hướng di chuyển ngẫu nhiên
    void ChooseNewDirection()
    {
        moveSpeed = 1f;
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        movement = new Vector2(randomX, randomY).normalized;
    }

    // Hàm đuổi theo nhân vật
    void ChasePlayer()
    {
        moveSpeed = 1.5f;
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        movement = directionToPlayer;
    }
}
