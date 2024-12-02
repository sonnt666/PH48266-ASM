using UnityEngine;

public class AtkZone : MonoBehaviour
{
    void Start()
    {
        // Hủy vùng tấn công sau 0.2 giây
        Destroy(gameObject, 0.2f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu đối tượng va chạm có tag là "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Hủy đối tượng Enemy
            Destroy(other.gameObject, 0.05f);
        }
    }
}