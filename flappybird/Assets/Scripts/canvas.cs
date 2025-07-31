using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class canvas : MonoBehaviour
{
    public Text scoreText;
    public Button restartButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateScoreUI(int score)
    {
        scoreText.text = "Score: " + score;
    }
    void RestartGame()
    {
        GameManager.Instance.Restart();
    }


}
