using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Coroutine shootRoutine;
    //public GameObject bullet;
    private int heart = 3;
    public GameObject shipShield;

    public int bulletCount = 1; // số viên đạn bắn ra ban đầu
    public float angleStep = 15f; // khoảng cách giữa các viên đạn
    public float shootInterval = 0.5f;
    public GameObject[] bulletList;
    private int currentBulletIndex = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ShootAtChicken2());
    }

    // Update is called once per frame
    void Update()
    {
        // Di chuyển trái phải 
        float move1 = Input.GetAxisRaw("Horizontal");
        float move2 = Input.GetAxisRaw("Vertical");

        rb.linearVelocity = new Vector2(move1 * moveSpeed, rb.linearVelocity.y);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, move2 * moveSpeed);

        Vector3 TopLeftPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height, 0));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -TopLeftPoint.x + 0.7f, TopLeftPoint.x - 0.7f),
            Mathf.Clamp(transform.position.y, -TopLeftPoint.y + 0.7f, TopLeftPoint.y - 0.7f), transform.position.z);


        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    shootRoutine = StartCoroutine(ShootAtChicken());
        //}
        
        if(bulletCount > 3)
        {
            currentBulletIndex++;
            AudioManager.instance.playSFX("levelUp");
            bulletCount = 1;
        }
    }
    //IEnumerator ShootAtChicken()
    //{
    //    // Tạo viên đạn
    //    GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);

    //    // Gán hướng bay thẳng lên trên
    //    Bullet bulletScript = temp.GetComponent<Bullet>();
    //    if (bulletScript != null)
    //    {
    //        Vector3 dir = Vector2.up;
    //        bulletScript.SetDirection(dir);
    //    }
    //    yield return new WaitForSeconds(shootInterval);
    //}
    IEnumerator ShootAtChicken2()
    {
        while (true)
        {
            // nếu có nhiều viên đạn thì trải đều góc
            float startAngle = -(bulletCount - 1) / 2f * angleStep;
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = startAngle + i * angleStep;
                Quaternion rotation = Quaternion.Euler(0, 0, angle);
                Instantiate(bulletList[currentBulletIndex], transform.position, rotation);
            }

            yield return new WaitForSeconds(shootInterval);
        }

        //while (true)
        //{
        //    float[] spreadAngles = { -15f, 0f, 15f };
        //    foreach (float angle in spreadAngles)
        //    {
        //        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        //        Instantiate(bulletList[currentBulletIndex], transform.position, rotation);
        //    }
        //    yield return new WaitForSeconds(shootInterval);
        //}

        //while (true)
        //{
        //    Quaternion rotation = Quaternion.Euler(0, 0, 45f);
        //    Instantiate(bulletList[currentBulletIndex], transform.position, Quaternion.identity);
        //    yield return new WaitForSeconds(shootInterval);
        //    Instantiate(bulletList[currentBulletIndex], transform.position, rotation);
        //    yield return new WaitForSeconds(shootInterval);
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && shipShield==null)
        {
            StartCoroutine(FlashRed());
            if (heart > 0)
            {
                heart = heart - 1;
                GameManager.Instance.MinusHeart(1);
            }
            else if (heart == 0)
            {
                AudioManager.instance.playSFX("dead");
                GameManager.Instance.GameOver();
            }
        }
        if(collision.gameObject.CompareTag("Special"))
        {
            bulletCount++;
            Destroy(collision.gameObject);
        }
    }
    IEnumerator FlashRed()
    {
        SpriteRenderer player = GetComponent<SpriteRenderer>();
        player.color = Color.red; // đổi sang đỏ
        yield return new WaitForSeconds(0.2f); // chờ 1 giây
        player.color = Color.white; // đổi lại màu gốc
    }
}

