using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour 
{
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;

    public void LoadLevel (int minutesIndex)
    {
        StateIntController.minutes = minutesIndex;
        StartCoroutine(LoadAsynchronously(1));
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressText.text = $"Loading Game, Please wait ({progress * 100:00.00}%)";
            yield return null;
        }
    }
}
