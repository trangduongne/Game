using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameOverPanel;
    public bool isGameOver = false;
    private int sum = 0;   // Biến lưu điểm tổng
    public Text diemne;
    public canvas canvasScript; 

    void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void AddScore(int score)
    {
        sum += score;
        Debug.Log("Score: " + sum);
        diemne.text = "Score: " + sum;
        canvasScript.UpdateScoreUI(sum);
    }
}
