using UnityEngine;

public class movebg : MonoBehaviour
{
    public float speed;
    public GameObject secondBackground;  // Tham chiếu đến background thứ 2

    private float backgroundWidth;

    void Start()
    {
        // Tính chiều rộng của background
        backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Dịch chuyển sang trái với tốc độ "speed" theo thời gian
        transform.position += Vector3.left * speed * Time.deltaTime;
        // Kiểm tra nếu background đã ra ngoài màn hình
        if (transform.position.x <= -backgroundWidth)
        {
            // Đưa background trở lại vị trí ban đầu
            transform.position = new Vector3(transform.position.x + backgroundWidth * 2, transform.position.y, transform.position.z);
        }

        // Di chuyển background thứ 2
        secondBackground.transform.position += Vector3.left * speed * Time.deltaTime;

        // Kiểm tra nếu background thứ 2 đã ra ngoài màn hình
        if (secondBackground.transform.position.x <= -backgroundWidth)
        {
            // Đưa background thứ 2 trở lại vị trí ban đầu
            secondBackground.transform.position = new Vector3(secondBackground.transform.position.x + backgroundWidth * 2, secondBackground.transform.position.y, secondBackground.transform.position.z);
        }
        
    }
}
