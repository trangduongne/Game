using UnityEngine;

public class moveLeft : MonoBehaviour
{
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Dịch chuyển ống nước sang trái với tốc độ "speed" theo thời gian
        transform.position += Vector3.left * speed * Time.deltaTime;
        // Kiểm tra nếu ống ra ngoài màn hình, thì xóa ống
        if (transform.position.x < -10f)  // Điều chỉnh -10f sao cho phù hợp với màn hình của bạn
        {
            Destroy(gameObject);  // Xóa ống khi nó ra ngoài màn hình
        }
        
    }
}
