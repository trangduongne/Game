using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class canvasController2 : MonoBehaviour
{
    public TMP_Text scoreText2;
    public Button restartButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
        scoreText2.text = "Score: " + 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateScoreUI(int score)
    {
        scoreText2.text = "Score: " + score;
        Debug.Log("canvasController2: " + scoreText2.text);
        Debug.Log("Updated UI Score: " + score);
    }
    void RestartGame()
    {
        Controller.Instance.Restart();
    }
}
