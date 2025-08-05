using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
 using TMPro;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameOverPanel;
    public GameObject start;
    [SerializeField] public Button startgame;
    public bool isGameOver = false;
    private int sum = 0;   // Biến lưu điểm tổng
    public Text diemne;
    public canvas canvasScript;
    [SerializeField] AudioSource startsound;
    [SerializeField] AudioSource gameoversound;
    public TMP_Text countdownText;

    void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(false);
        countdownText.gameObject.SetActive(false);
        start.SetActive(true);
        startgame.onClick.AddListener(GameStart);  
    }

    // Update is called once per frame
    void Update()
    {

    }
public void GameStart()
{
    start.SetActive(false);
    startsound.Play();
    StartCoroutine(DelayGameStart());
}

IEnumerator DelayGameStart()
{

    countdownText.gameObject.SetActive(true); // hiện text

    for (int i = 3; i > 0; i--)
    {
        countdownText.text = i.ToString();
        yield return new WaitForSecondsRealtime(1f);
    }

    countdownText.gameObject.SetActive(false); // ẩn sau khi đếm
    Time.timeScale = 1;

}

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        gameoversound.Play();
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
