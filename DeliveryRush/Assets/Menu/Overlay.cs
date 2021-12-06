using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlay : MonoBehaviour
{
    public Transform box;
    public CanvasGroup background;
    public GameObject bg;

    private void OnEnable()
    {
        background.alpha = 0;
        background.LeanAlpha(1, 0.5f);
        box.localPosition = new Vector2(Screen.width, 0);
        box.LeanMoveX(Screen.width - (Screen.width/6), 0.5f).setEaseOutExpo().delay = 0.1f;
    }

    public void CloseDialog()
    {
        background.LeanAlpha(0, 0.5f);
        box.LeanMoveX(Screen.width, 0.5f).setEaseInExpo().setOnComplete(onComplete);

    }

    void onComplete()
    {
        gameObject.SetActive(false);
        bg.SetActive(false);
    }
}
