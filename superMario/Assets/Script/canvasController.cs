using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class canvasController : MonoBehaviour
{
    public TMP_Text scoreText;
    public Button restartButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
        //scoreText.text = "Score: " + 99;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScoreUI(int score)
    {
        scoreText.text = "Score: " + score;
        Debug.Log("canvasControllerr: " + scoreText.text);
        Debug.Log("Updated UI Score: " + score);
    }
    void RestartGame()
    {
        Controller.Instance.Restart();
    }
}
