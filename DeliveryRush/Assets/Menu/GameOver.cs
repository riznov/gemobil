using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public Player ply;
    private bool gameOver = false;
    public CanvasGroup UIGameOverBG;
    public GameObject UIGameOverMenu;
    public Transform UIGameOverBox;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI MaxComboText;

    private int score, max_combo;

    private void Start()
    {
        gameOver = false;
    }
    void Update()
    {
        if((ply.MainTimer <= 0.0f || ply.isibensin <= 0.2f) && !gameOver)
        {
            gameOver = true;
            GameOverUIPop();
        }
        if (ply.MainTimer <= 0.0f || ply.isibensin <= 0.2)
        {
            score += (int)(ply.MainScore*Time.fixedDeltaTime);
            if (score >= ply.MainScore)
                score = ply.MainScore;
            max_combo = ply.MaxCombo;
            ScoreText.text = $"{score}";
            MaxComboText.text = $"{max_combo}x";
        }
    }

    void GameOverUIPop()
    {
        UIGameOverMenu.SetActive(true);
        UIGameOverBG.alpha = 0;
        UIGameOverBG.LeanAlpha(1, 0.2f);
        UIGameOverBox.localScale = new Vector2(1, 0);
        UIGameOverBox.LeanScaleY(1f, 0.2f).setEaseOutExpo().delay = 0.1f;
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackToMenu()
    {
        FindObjectOfType<GameManager>().SwitchSceneAsyncronously(0);
    }
}
