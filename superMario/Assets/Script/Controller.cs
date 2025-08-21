using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Controller : MonoBehaviour
{
    public static Controller Instance;
    private int sum = 0;   // Biến lưu điểm tổng
    int heart = 3;
    public TMP_Text diemne;
    public TMP_Text maune;
    public GameObject gameOverPanel;
    public GameObject gameWinPanel;
    public GameObject start;
    [SerializeField] public Button startgame;
    public bool isGameOver = false;
    public bool isGameWin = false;
    public canvasController canvasControllerr;
    public canvasController2 canvasController2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(false);
        gameWinPanel.SetActive(false);
        start.SetActive(true);
        maune.text = "Heart: " + heart;
        diemne.text = "Score: " + sum;
        startgame.onClick.AddListener(GameStart);
    }
    public void GameStart()
    {
        start.SetActive(false);
        Time.timeScale = 1;
    }
    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }
    public void GameWin()
    {
        isGameWin = true;
        Time.timeScale = 0;
        gameWinPanel.SetActive(true);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void AddScore(int score)
    {
        sum += score;
        Debug.Log("Score: " + sum);
        diemne.text = "Score: " + sum;
        canvasControllerr.UpdateScoreUI(sum);
        canvasController2.UpdateScoreUI(sum);
    }
    public void MinusHeart(int damage)
    {
        heart -= damage;
        maune.text = "Heart: " + heart;

        if (heart <= 0 && !isGameOver)
        {
            GameOver();
        }
    }

}
