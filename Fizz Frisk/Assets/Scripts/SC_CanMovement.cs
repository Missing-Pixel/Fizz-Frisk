using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CanMovement : MonoBehaviour
{
    // Variables
    public float waitTime = 0f;
    public float runSpd = 1f;
    private bool waitComplete = true;

    void Update()
    {
        ConveyerMovement();
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
