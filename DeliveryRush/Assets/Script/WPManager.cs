using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int maxspawn { get; set; } = 10; 
    public GameObject packetrev;

    private Transform[] spawnPos;

    bool checkIfArrayIsUnique(int[] myArray,int len_check)
    {
        for (int l = 0; l <= len_check; l++)
        {
            for (int j = 0; j <= len_check; j++)
            {
                if (l != j)
                {
                    if (myArray[l] == myArray[j])
                    {
                        return true; // ada value dobel
                    }
                }
            }
        }
        return false; // ga ada value dobel, sip gas.
    }

    public void SetActiveWaypoint()
    {
        int i = 0;
        //Debug.Log("SpawnposLenght = " + spawnPos.Length);
        int[] spawnpospos = new int[spawnPos.Length];
        while (i < maxspawn)
        {
            spawnpospos[i] = Random.Range(0, spawnPos.Length);
            if (i > 0 && checkIfArrayIsUnique(spawnpospos, i))
            {
                //ada yg sama? ulangi lagi
                //Debug.Log("Spawnpos (" + i + ") = " + spawnpospos[i]);
                //Debug.Log("Spawnpos keduplikat bang !!");
                continue;
            }
            Instantiate(packetrev, spawnPos[spawnpospos[i]]);
            //Debug.Log("Spawnpos (" + i + ") = " + spawnpospos[i]);
            i++;
        }
    }

    void Start()
    {
        spawnPos = GameObject.Find("CheckpointsLocations").GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
