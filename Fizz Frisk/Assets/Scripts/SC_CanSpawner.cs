using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CanSpawner : MonoBehaviour
{
    //Variables
    public float canInterval = 3f;
    private float canTimer = 0f;
    private bool hasrepeated = true;
    public int preventRepeatBad = 0;
    public int preventRepeatGood = 0;

    public GameObject[] canNum;
    public Animator anim;

    void FixedUpdate()
    {
        //Spawn Timer
        canTimer += Time.fixedDeltaTime;

        //Rigs can spawning RNG
        if (preventRepeatBad > 1 || preventRepeatGood > 2)
        {
            hasrepeated = true;
        }

        if (canTimer >= canInterval)
        {
            //Rigs the rng to spawn a good can
            if (hasrepeated == true && preventRepeatBad > 1)
            {
                preventRepeatBad = 0;
                Instantiate(canNum[0], transform.position, transform.rotation);
                hasrepeated = false;
                anim.SetBool("WaitEnd", true);
            }
            //Rigs the rng to spawn a bad can
            else if (hasrepeated == true && preventRepeatGood > 1)
            {
                preventRepeatGood = 0;
                Instantiate(canNum[canNum.Length - 1], transform.position, transform.rotation);
                hasrepeated = false;
                anim.SetBool("WaitEnd", true);
            }
            //Spawns a random can
            else if (hasrepeated == false)
            {
                Instantiate(canNum[Random.Range(0, canNum.Length)], transform.position, transform.rotation);
                anim.SetBool("WaitEnd", true);
            }

            canTimer -= canInterval + Time.fixedDeltaTime;
        }
        else if (canTimer <= canInterval)
        {
            anim.SetBool("WaitEnd", false);
        }
    }

    //increases a variable when the same can is spawned multiple times in a row
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Danger")
        {
            preventRepeatGood = 0;
            preventRepeatBad += 1;
        }
        else if (collision.tag == ("Safe"))
        {
            preventRepeatBad = 0;
            preventRepeatGood += 1;
        }
    }
}
