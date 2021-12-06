using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Mainmenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Transform menuBox;
    // Start is called before the first frame update

    public void SetVolumeBGM(float val)
    {
        audioMixer.SetFloat("VolumeParam", val);
    }
    void Start()
    {
        menuBox.localPosition = new Vector2(-Screen.width, 0);
        menuBox.LeanMoveX(Screen.width/5, 0.5f).setEaseOutExpo().delay = 0.1f;
    }

    // Update is called once per frame
    public void Quitgame()
    {
        Application.Quit();
        //Debug.Log("App Quit");
    }
}
