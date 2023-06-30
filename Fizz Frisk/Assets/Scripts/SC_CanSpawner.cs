using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CanSpawner : MonoBehaviour
{
    //Variables
    public float canInterval = 3f;
    private float canTimer = 0f;

    public GameObject[] canNum;

    void Update()
    {
        //Spawn Timer
        canTimer += Time.deltaTime;

        if (canTimer >= canInterval)
        {
            Instantiate(canNum[Random.Range(0, canNum.Length)]);
            canTimer -= canInterval;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tag == "Danger")
        {
            //Create a list to check if 2 danger are created, then make good cans before reseting the list
        }
    }
}
