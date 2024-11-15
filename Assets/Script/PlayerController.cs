using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AnimationClip clip;
    public PlayerStat stat;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 mm;
    private bool facingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
}
