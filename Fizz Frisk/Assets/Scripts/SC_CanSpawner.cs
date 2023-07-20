using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CanSpawner : MonoBehaviour
{
    //Variables
    public float canInterval = 3f;
    private float canTimer = 0f;
    private bool hasrepeated = true;
    public int preventRepeat = 2;

    public GameObject[] canNum;
    public Animator anim;

    void Update()
    {
        //Spawn Timer
        canTimer += Time.deltaTime;

        if (canTimer >= canInterval)
        {
            if (hasrepeated == true)
            {
                Instantiate(canNum[0], transform.position, transform.rotation);
                preventRepeat = 0;
                hasrepeated = false;
                anim.SetBool("WaitEnd", !anim.GetBool("WaitEnd"));
            }
            else if (hasrepeated == false)
            {
                Instantiate(canNum[Random.Range(0, canNum.Length)], transform.position, transform.rotation);
                anim.SetBool("WaitEnd", !anim.GetBool("WaitEnd"));
                
            }

            canTimer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (preventRepeat > 0)
        {
            hasrepeated = true;
        }

        if (collision.tag == "Danger")
        {
            preventRepeat += 1;
        }
    }
}
