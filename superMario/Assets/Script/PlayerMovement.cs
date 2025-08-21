using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;        // Tốc độ di chuyển
    public float jumpForce = 7f;        // Lực nhảy
    private Rigidbody2D rb;
    Animator animator;
    public int maxJumps = 3;
    private int jumpCount = 0;
    private int heart = 3; // Số lượng máu ban đầu
    public GameObject gameOverPanel; // Panel hiển thị khi game over

    private bool isGrounded = false;
    bool hitHead = false;

    public float fallThreshold = -10f; // vị trí y thấp nhất

    [SerializeField] AudioSource jump;
    [SerializeField] AudioSource coin;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        // Di chuyển trái/phải
        float move = Input.GetAxisRaw("Horizontal");
        if (move != 0)
        {
            animator.SetBool("IsRun", true);
        }
        else
        {
            animator.SetBool("IsRun", false);
        }

        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        // Nhảy
        if (Input.GetButtonDown("Jump") && (isGrounded || jumpCount < maxJumps))
        {
            jump.Play();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // reset tốc độ Y
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("IsJump");
            jumpCount++;
        }


        // Quay mặt theo hướng di chuyển
        if (move > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Quay mặt sang phải
        }
        else if (move < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Quay mặt sang trái
        }

        if (transform.position.y < fallThreshold)
        {
           // Debug.Log("Player rơi xuống vực!");
            Controller.Instance.GameOver();
        }
    }

    // Khi rời khỏi mặt đất
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    IEnumerator FlashRed()
    {
        SpriteRenderer player = GetComponent<SpriteRenderer>();
        player.color = Color.red; // đổi sang đỏ
        yield return new WaitForSeconds(0.2f); // chờ 1 giây
        player.color = Color.white; // đổi lại màu gốc
    }


    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0; // reset số lần nhảy khi chạm đất
        }


        if (collision.collider.CompareTag("Enemy"))
        {
            if (hitHead == true)
            {
                // đã nhảy trúng đầu thì KHÔNG trừ máu
                hitHead = false;
                return;
            }

            // nếu không nhảy trúng đầu thì mới trừ máu
            StartCoroutine(FlashRed());
            if (heart > 0)
            {
                heart = heart - 1;
                Controller.Instance.MinusHeart(1);
            }
            else if (heart == 0)
            {
                Controller.Instance.GameOver();
            }
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra va chạm với đồng xu
        if (collision.CompareTag("Coin"))
        {
            coin.Play(); // Phát âm thanh khi thu thập đồng xu
            // Thêm điểm số hoặc thực hiện hành động khi thu thập đồng xu
            Controller.Instance.AddScore(10); // Giả sử mỗi đồng xu có giá trị 10 điểm
            Destroy(collision.gameObject); // Hủy đối tượng đồng xu
        }

        GameObject enemy = collision.transform.parent != null
            ? collision.transform.parent.gameObject
            : collision.gameObject;  // fallback nếu không có parent

        SpriteRenderer sr = enemy.GetComponent<SpriteRenderer>();

        // Kiểm tra khi rời khỏi vùng va chạm với đầu quái vật
        if (collision.CompareTag("EnemyHead"))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
            hitHead = true; // Đánh dấu đã va chạm với đầu quái vật
            if (sr != null)
            {
                sr.color = Color.red;
            }
            Destroy(enemy,0.2f); // Hủy đối tượng kẻ thù khi rời khỏi vùng va chạm

        }
    }
}
