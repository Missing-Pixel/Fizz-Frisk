using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CanMovement : MonoBehaviour
{
    // Variables
    public float waitTime = 0f;
    public float runSpd = 1f;
    private bool waitComplete = true;

    [HideInInspector] public bool deleteSelf = false;
    public float deletionTime = 1f;
    private bool deletionComplete = false;

    void Update()
    {
        ConveyerMovement();

        if (deleteSelf == true)
        {
            if (deletionComplete == true)
            {
                Destroy(gameObject);
            }

            if (deletionComplete == false && deletionTime > 0)
            {
                deletionTime -= Time.deltaTime;
            }
            else if (deletionTime <= 0)
            {
                deletionComplete = true;
            }

        }
    }

    void ConveyerMovement()
    {
        if (waitComplete == true)
        {
            transform.Translate(Vector2.right * runSpd * Time.deltaTime);
            StartCoroutine(WaitRest(waitTime));
        }
        else if (waitComplete == false)
        {
            StartCoroutine(WaitRest(waitTime));
        }
    }

    IEnumerator WaitRest(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        waitComplete = !waitComplete;
        StopAllCoroutines();
    }
}
