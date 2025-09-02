using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject Player;
    public GameObject canvas;
    public GameObject gameOverPanel;
    public GameObject gameWinPanel;
    public GameObject start;
    [SerializeField] public Button startgame;
    public bool isGameOver = false;
    public bool isGameWin = false;
    private int sum = 0;
    [SerializeField] public int heart = 3;
    private int killed_enemies;
    public TMP_Text diemne;
    public TMP_Text maune;
    public Button restartButton;
    public Button restartButton2;
    [SerializeField] public int killed_chicken =0 ;
    public GameObject boom;
    private float boomDuration = 0.24f;



    void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(false);
        gameWinPanel.SetActive(false);
        start.SetActive(true);
        maune.text = "Heart: " + heart;
        diemne.text = "Score: " + sum;
        startgame.onClick.AddListener(GameStart);
        restartButton.onClick.AddListener(Restart);
        restartButton2.onClick.AddListener(Restart);
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
        AudioManager.instance.playSFX("Gameover");
    }
    public void GameWin()
    {
        isGameWin = true;
        Time.timeScale = 0;
        gameWinPanel.SetActive(true);
        AudioManager.instance.playSFX("Gamewin");
    }
    public void Restart()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
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
    }
    public void MinusHeart(int damage)
    {
        heart -= damage;
        maune.text = "Heart: " + heart;

        if (heart <= 0 && !isGameOver)
        {
            if (boom != null)
            {
                GameObject bom = Instantiate(boom, Player.transform.position, Quaternion.identity);
                Destroy(Player);
            }
            StartCoroutine(DelayedGameOver(boomDuration));
        }
    }
    private IEnumerator DelayedGameOver(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameOver();
    }


    //public void totalchickens(int total)
    //{
    //    if(total==0)
    //    {
    //        GameWin();
    //    }
    //}
    public int ChickenKilled(int killedenemies)
    {
        return killed_chicken += killedenemies;
    }

    public void boss(int number)
    {
        if (number == 0)
        {
            GameWin();
        }
    }
}
