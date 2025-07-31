using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipe;
    public float maxTime;//khoảng thời gian giữa mỗi lần sinh ra 1 ống nc
    float timer;
    public float height;//khoảng random range 

    public float spacing = 4f; // Khoảng cách giữa các ống trên trục X
    public GameObject bird;
    private float lastPipeX;  // Vị trí X của ống cuối cùng đã sinh ra


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = maxTime; // tạo 1 ống khi bắt đầu game
        lastPipeX = bird.transform.position.x +3f;  // Đặt ống đầu tiên ở vị trí chim
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > maxTime)
        {
            float spawnY = bird.transform.position.y + Random.Range(-height, height);  // Random vị trí Y của ống

            // Sinh ống mới
            Vector3 spawnPosition = new Vector3(lastPipeX, spawnY, 0);
            GameObject temp = Instantiate(pipe, spawnPosition, Quaternion.identity);

            
            float spawnX = lastPipeX + spacing;  // Các ống sẽ sinh ra từ vị trí X của ống trước đó
            // Cập nhật vị trí X của ống cuối cùng đã sinh ra
            lastPipeX = spawnX;

            // Gán một script di chuyển và xóa cho ống mới
            temp.AddComponent<moveLeft>();  // Thêm một component mới để xử lý di chuyển và xóa ống

            timer = 0;
        }
        timer += Time.deltaTime;
    }
}
