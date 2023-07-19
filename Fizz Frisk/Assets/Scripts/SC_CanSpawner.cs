using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CanSpawner : MonoBehaviour
{
    //Variables
    public float canInterval = 3f;
    private float canTimer = 0f;
    private Transform test;
    private bool hasrepeated = true;

    public GameObject[] canNum;
    /*[HideInInspector]*/ public List<GameObject> preventRepeat = new List<GameObject>();

    void Update()
    {
        //Spawn Timer
        canTimer += Time.deltaTime;

        if (canTimer >= canInterval)
        {
            if (hasrepeated == true)
            {
                Instantiate(canNum[0], transform.position, transform.rotation);
                preventRepeat.Clear();
                hasrepeated = false;
            }
            else if (hasrepeated == false)
            {
                Instantiate(canNum[Random.Range(0, canNum.Length)], transform.position, transform.rotation);
            }

            canTimer -= canInterval;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (preventRepeat.Count >= 1)
        {
            hasrepeated = true;
        }

        preventRepeat.Add(canNum[1]);
    }
}
