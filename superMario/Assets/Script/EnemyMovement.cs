using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform pointA; // Điểm A
    public Transform pointB; // Điểm B
    public float speed = 2f; // Tốc độ di chuyển
    private Vector3 targetPosition; // Vị trí mục tiêu hiện tại

    void Start()
    {
        targetPosition = pointB.position; // Bắt đầu di chuyển đến điểm B
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Kiểm tra nếu đã đến điểm A hoặc B
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SwitchTarget(); // đổi hướng
        }
    }

    private void SwitchTarget()
    {
        if (targetPosition == pointB.position)
        {
            targetPosition = pointA.position;
            transform.localScale = new Vector3(-1, 1, 1); // Quay trái
        }
        else
        {
            targetPosition = pointB.position;
            transform.localScale = new Vector3(1, 1, 1); // Quay phải
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Khi va chạm player thì đổi hướng liền
            SwitchTarget();
        }
    }
}
