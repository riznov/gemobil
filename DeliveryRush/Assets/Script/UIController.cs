using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject GamePlayUI;
    public GameObject CountDown;
    public GameObject LoadingBar,UnloadBar;
    public GameObject FuelTex;
    public GameObject waveTex;

    public Text UITimerText;
    public Text UIScoreText;
    public Text UIComboText;
    public Text UIGoodsLowText;
    public Text UIGoodsHighText;
    public TextMeshProUGUI countDownText;
    public TextMeshProUGUI waveText;

    public Player player;
    public WPManager Wpm;

    private float UIMainTimer;
    private int UIScore;
    private int UICombo;
    private int GoodsLow;
    private int GoodsHigh;
    private float countDown = 4;

    private bool isLoading,isUnloading, cdOn, cdOff;
    private UIBensinBar UILoadingBar,UIUnloadingBar;
    bool UILoadBarOpened, UIUnLoadBarOpened, waveOpened;



    private void Start()
    {
        cdOff = false;
        cdOn = false;
        UIScoreText.text = $"0000";
        UIComboText.text = $"1x";
        UIGoodsLowText.text = $"...";
        UIGoodsHighText.text = $"...";
        LoadingBar.SetActive(false);
        UnloadBar.SetActive(false);
        GamePlayUI.SetActive(false);
        CountDown.SetActive(true);
        waveTex.SetActive(false);
        UILoadingBar = LoadingBar.GetComponentInChildren<UIBensinBar>();
        UIUnloadingBar = UnloadBar.GetComponentInChildren<UIBensinBar>();
        UILoadBarOpened = false;
        UIUnLoadBarOpened = false;
        waveOpened = false;
        countDown = 4;
    }

    void SetCountDownOff()
    {
        CountDown.SetActive(false);
        GamePlayUI.SetActive(true);
    }

    private void Update()
    {
        if (player == null) return;


        //wave
        if (!waveOpened && player.pickupStart)
        {
            waveTex.SetActive(true);
            waveTex.GetComponentInChildren<Transform>().localScale = new Vector2(0, 1);
            waveTex.GetComponentInChildren<Transform>().LeanScaleX(1f, 0.5f).setEaseOutExpo().delay = 0.1f;
            waveOpened = true;
        }
        waveText.text = $"WAVE x{player.wave}";

        //CountDown===========================================================================
        if (!cdOn)
        {
            cdOn = true;
            player.NotYetStarted = true;
            CountDown.GetComponentInChildren<Transform>().localScale = new Vector2(1, 0);
            CountDown.GetComponentInChildren<Transform>().LeanScaleY(1f, 0.5f).setEaseOutExpo().delay = 0.1f;
            
        }
        countDown -= Time.deltaTime;
        if(countDown < 1)
            countDownText.text = $"GO!!";
        else
            countDownText.text = $"{(int)countDown}";

        if (countDown <= 0 && !cdOff)
        {
            player.NotYetStarted = false;
            cdOff = true;
            CountDown.GetComponentInChildren<Transform>().LeanScaleY(0f, 0.1f).setEaseOutExpo().setOnComplete(SetCountDownOff);
        }

        //refuel===========================================================================
        FuelTex.SetActive(player.isRefuel);

        //MainTimer===========================================================================
        if (player.MainTimer != UIMainTimer)
        {
            UIMainTimer = player.MainTimer;
            if(UIMainTimer/60<10)
                UITimerText.text = $"0{(int)UIMainTimer / 60}:{(UIMainTimer) % 60:00}";
            else
                UITimerText.text = $"{(int)UIMainTimer / 60}:{(UIMainTimer) % 60:00}";

        }

        //Combo===========================================================================

        if (player.MainCombo != UICombo)
        {
            UICombo = player.MainCombo;
            UIComboText.text = $"x{UICombo}";
        }

        //Score===========================================================================
        if (player.MainScore != UIScore)
        {
            UIScore += 10;
            UIScoreText.text = $"{UIScore}";
        }

        //Goods===========================================================================
        if (Wpm.maxspawn != GoodsHigh && UIScore > 999)
        {
            GoodsHigh = Wpm.maxspawn;
            UIGoodsHighText.text = $"{GoodsHigh}";
        }
        if (player.packetCount != GoodsLow && UIScore > 999)
        {
            GoodsLow = player.packetCount;
            UIGoodsLowText.text = $"{GoodsHigh - GoodsLow}";
            if(Wpm.maxspawn > 5 && (GoodsHigh - GoodsLow) == Wpm.maxspawn)
            {
                UIGoodsLowText.text = $"...";
            }
        }


        //Loading Goods Bar===========================================================================
        if (player.inLoadMode != isLoading)
            isLoading = player.inLoadMode;

        if(isLoading && player.packetCount == 0)
        {
            LoadingBar.SetActive(true);
            if (!UILoadBarOpened) {
                UILoadBarOpened = true;
                LoadingBar.GetComponentInChildren<Transform>().localScale = new Vector2(0, 1);
                LoadingBar.GetComponentInChildren<Transform>().LeanScaleX(1f, 0.5f).setEaseOutExpo().delay = 0.1f;
            }
            UILoadingBar.SetHealth(player.LoadMode);
        }
        else
            LoadingBar.SetActive(false);

        if(!isLoading)
            UILoadBarOpened = false;

        //UNloading Goods Bar===========================================================================
        if (player.inUnloadMode != isUnloading)
            isUnloading = player.inUnloadMode;

        if (isUnloading)
        {
            UnloadBar.SetActive(true);
            if (!UIUnLoadBarOpened)
            {
                UIUnLoadBarOpened = true;
                UnloadBar.GetComponentInChildren<Transform>().localScale = new Vector2(0, 1);
                UnloadBar.GetComponentInChildren<Transform>().LeanScaleX(1f, 0.5f).setEaseOutExpo().delay = 0.1f;
            }
            UIUnloadingBar.SetHealth(player.UnLoadMode);
        }
        else
            UnloadBar.SetActive(false);

        if (!isUnloading)
            UIUnLoadBarOpened = false;
    }

}
