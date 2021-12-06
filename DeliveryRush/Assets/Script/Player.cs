using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public WPManager WpManager;
    public UIBensinBar BensinBar;
    public UIBensinBar NOSBar;
    private PacketReceiver packet;

    public int MainScore { get; set; } = 0;
    public int MainCombo { get; set; } = 1;
    public int MaxCombo { get; set; } = 1;
    public int packetCount { get; set; } = 0;
    public float MainTimer { get; set; } = 0f;
    public float isibensin { get; set; } = 500f;
    public bool NotYetStarted { get; set; }
    //Wave Counter dan UI
    public int wave = 1;
    public bool pickupStart = false;

    public int MinutesTimer = 0;
    public bool isRefuel = false;
    public bool inLoadMode = false;
    public bool inUnloadMode = false;
    public float LoadMode = 0.0f;
    public float UnLoadMode = 0.0f;

    private Car carController;
    private float nos_val;
    private float combo_timer = 0f;

    private void OnTriggerStay(Collider other)
    {
        packet = other.gameObject.GetComponent<PacketReceiver>();
        if (packet != null)
           inUnloadMode = true;
    }
    void OnTriggerEnter(Collider other)
    {
        packet = other.gameObject.GetComponent<PacketReceiver>();
        if (packet != null)
        {
            // waypoint = active?
            inUnloadMode = true;
        }
        //loaddetect
        if (other.gameObject.name == "load")
            inLoadMode = true;

        if (other.gameObject.name == "bensin")
            isRefuel = true;
    }

    void OnTriggerExit(Collider other)
    {
        packet = other.gameObject.GetComponent<PacketReceiver>();
        if (packet != null)
        {
            inUnloadMode = false;
        }
        //loaddetect
        if (other.gameObject.name == "load")
            inLoadMode = false;

        if (other.gameObject.name == "bensin")
            isRefuel = false;

    }

    void Awake()
    {
        carController = GetComponent<Car>();
    }
    // Start is called before the first frame update
    void Start()
    {
        MinutesTimer = StateIntController.minutes;
        //MinutesTimer = 1;
        //TimerPlay
        MainTimer = (60f * (float)MinutesTimer) + 4f;
        combo_timer = 0f;
        isibensin = carController.bensin;
        wave = 1;
        pickupStart = false;
}

    // Update is called once per frame
    void Update()
    {
        //Update Timer
        if(MainTimer > 0f) MainTimer -= Time.deltaTime;
        combo_timer += Time.deltaTime;
        if (combo_timer > 10f)
            MainCombo = 1;
        // Debug.Log("MainTimer = " + MainTimer);

        isibensin = carController.bensin;
        nos_val = carController.fNos;
        BensinBar.SetHealth(isibensin);
        NOSBar.SetHealth(nos_val);

        //Game Over & Belum Mulai?
        if (MainTimer <= 0.0f || NotYetStarted)
            carController.MsgMultSpeed = 0f;
        else
            carController.MsgMultSpeed = 1f;

        //Pickup
        if (inLoadMode && packetCount == 0)
        {
            if (Input.GetKey(KeyCode.F))
            {
                LoadMode += (Time.deltaTime*80);
                if (LoadMode >= 500.0f)
                {
                    WpManager.SetActiveWaypoint();
                    pickupStart = true;
                    FindObjectOfType<AudioManager>().PlaySound("Pickup");
                    packetCount = WpManager.maxspawn;
                    LoadMode = 500.0f;
                    MainScore += 1000;
                }
            }
        }
        else
        {
            LoadMode = 0.0f;
        }

        //Unload
        if (inUnloadMode)
        {
            if (Input.GetKey(KeyCode.F))
            {
                UnLoadMode += (Time.deltaTime * 500);
                if (UnLoadMode >= 500.0f)
                {
                    
                    UnLoadMode = 0.0f;
                    packetCount -= 1;
                    //waveend
                    if (packetCount <= 0)
                    {
                        wave++;
                        WpManager.maxspawn += 5;
                        FindObjectOfType<AudioManager>().PlaySound("Pickup");
                    }
                    //bonus timer
                    MainTimer += 5f;
                    //scoring
                    if (combo_timer < 10f)
                        MainCombo++;
                    if (MaxCombo < MainCombo)
                        MaxCombo = MainCombo;
                    combo_timer = 0f;
                    MainScore = MainScore + (500 * MainCombo * wave);
                    packet.Destruction();
                    inUnloadMode = false;
                }
            }
            else
            {
                UnLoadMode -= 10.5f;
                if (UnLoadMode < 0.5f)
                    UnLoadMode = 0;
            }

        }
    }
}
