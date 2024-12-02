using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public AnimationClip clip;
    public PlayerStat stat;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 mm;
    private bool facingRight = true;
    public GameObject AtkZone;
    public TextMeshProUGUI itemCountText; // Tham chiếu đến UI Text trên Panel
    private int itemCount = 0; // Số lượng vật phẩm đã nhặt
    public GameObject bulletPrefab;           // Prefab của đạn (Bullet)
    public Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        UpdateItemCountUI();
    }

    // Update is called once per frame
    void Update()
    {
        mm.x = Input.GetAxisRaw("Horizontal");
        mm.y = Input.GetAxisRaw("Vertical");
        bool isRunning = mm.magnitude > 0.1f;
        anim.SetBool("isRunning", isRunning);
        if (mm.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (mm.x < 0 && facingRight)
        {
            Flip();
        }
        
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
            StartCoroutine(AttackAnimation());
        }
        WinGame();
        if (Input.GetKeyDown(KeyCode.K))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + mm * stat.speed * Time.fixedDeltaTime);
    }
    private void Flip()
    {
        // Đảo hướng của nhân vật
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void Attack()
    {
        // Tạo một vùng tấn công `AtkZone` tại vị trí của nhân vật
        GameObject atkzone = Instantiate(AtkZone, transform.position, Quaternion.identity, transform);

    }
    IEnumerator AttackAnimation()
    {
        anim.SetTrigger("IsAtk");
        yield return new WaitForSeconds(0.5f); // Thời gian của animation
        anim.SetTrigger("IsIdle");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Flower")) // Kiểm tra nếu va chạm là vật phẩm
        {
            Destroy(collision.gameObject); // Xóa vật phẩm khỏi màn hình
            itemCount++; // Tăng số lượng vật phẩm
            UpdateItemCountUI(); // Cập nhật UI
        }
    }

    private void UpdateItemCountUI()
    {
        if (itemCountText != null)
        {
            itemCountText.text = "Flower: " + itemCount;
        }
    }

    private void WinGame()
    {
        if (itemCount == 5)
        {
            SceneManager.LoadScene("menu");
        }
    }
    private void Shoot()
    {
        // Tạo đạn tại vị trí của firePoint và theo hướng nhân vật đang đối diện
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Đặt hướng của đạn dựa vào hướng của nhân vật
        BulletController bulletScript = bullet.GetComponent<BulletController>();
        bulletScript.SetDirection(facingRight ? Vector2.right : Vector2.left);
    }
}
