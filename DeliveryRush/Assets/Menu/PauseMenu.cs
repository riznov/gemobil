using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public CanvasGroup UIPauseBG;
    public GameObject UIPauseMenu;
    public Transform UIPauseBox;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                GameResume();
                //Debug.Log("Resume");
            }
            else
            {
                //Debug.Log("GamePaused");
                GamePause();
            }
        }
    }
    void GamePause()
    {
        GamePaused = true;
        UIPauseMenu.SetActive(true);
        UIPauseBG.alpha = 0;
        UIPauseBG.LeanAlpha(1, 0.2f);
        UIPauseBox.localScale = new Vector2(1, 0);
        UIPauseBox.LeanScaleY(1f, 0.2f).setEaseOutExpo().setOnComplete(PauseOnComplete);
        //Time.timeScale = 0f;
    }
    public void GameResume()
    {
        GamePaused = false;
        UIPauseBox.LeanScaleY(0, 0.2f).setEaseOutExpo().setOnComplete(ResumeOnComplete);
        UIPauseBG.LeanAlpha(0, 0.2f);
        Time.timeScale = 1f;
    }

    void ResumeOnComplete()
    {
        UIPauseMenu.SetActive(false);
    }
    void PauseOnComplete()
    {
        Time.timeScale = 0f;
    }
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        FindObjectOfType<GameManager>().SwitchSceneAsyncronously(0);
    }
}
