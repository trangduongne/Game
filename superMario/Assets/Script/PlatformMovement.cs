using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Transform pointA; // Điểm A
    public Transform pointB; // Điểm B
    public float speed = 2f; // Tốc độ di chuyển
    public Rigidbody rb;
    private Vector3 targetPosition; // Vị trí mục tiêu hiện tại
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPosition = pointB.position; // Bắt đầu di chuyển đến điểm B
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        // Kiểm tra nếu đã đến điểm A hoặc B
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Đổi mục tiêu
            if (Vector3.Distance(targetPosition, pointB.position) < 0.01f)
            {
                targetPosition = pointA.position; // Chuyển sang điểm A
            }
            else
            {
                targetPosition = pointB.position; // Chuyển sang điểm B
            }
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform); // Player trở thành con của platform
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null); // Player rời platform
        }
    }

}
