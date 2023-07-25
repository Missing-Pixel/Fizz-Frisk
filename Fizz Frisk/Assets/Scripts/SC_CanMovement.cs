using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CanMovement : MonoBehaviour
{
    // Variables
    public float waitTime = 0f;
    public float runSpd = 1f;
    private bool waitComplete = true;
    private bool avoidDelete = false;
    private bool objectSelected = false;
    private bool fixSelect = false;

    [HideInInspector] public bool deleteSelf = false;
    public float deletionTime = 1f;
    private bool deletionComplete = false;
    Material highlightShader;

    private void Start()
    {
        highlightShader = GetComponent<SpriteRenderer>().material;
    }

    void FixedUpdate()
    {
        //Move object to right, wait a few seconds, stop, wait same amount of time, then repeat
        if (waitComplete == true)
        {
            transform.Translate(Vector2.right * runSpd * Time.fixedDeltaTime);
            StartCoroutine(WaitRest(waitTime));
        }
        else if (waitComplete == false)
        {
            StartCoroutine(WaitRest(waitTime));
        }

        //Delete object after 5 seconds if deleteSelf = true
        if (deleteSelf == true)
        {
            if (deletionComplete == true)
            {
                Destroy(gameObject);
            }

            if (deletionComplete == false && deletionTime > 0)
            {
                deletionTime -= Time.fixedDeltaTime;
            }
            else if (deletionTime <= 0)
            {
                deletionComplete = true;
            }
        }

        //Destroy object on click, sends a signal for if the can was good or bad
        if (objectSelected == true)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (gameObject.tag == "Danger")
                {
                    Debug.Log("Killed Bad");
                }
                else if (gameObject.tag == "Safe")
                {
                    Debug.Log("Killed Good");
                }

                Destroy(gameObject);
            }
        }

        //prevent the can from being selectable if in X-Ray
        if (fixSelect == true)
        {
            if (avoidDelete == false)
            {
                highlightShader.SetInt("_OutlineOn", 1);
                objectSelected = true;
            }
        }
    }

    IEnumerator WaitRest(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        waitComplete = !waitComplete;
        StopAllCoroutines();
    }

    //Make the object selectable when mouse hovers, also used for outline shader.
    private void OnMouseEnter()
    {
        fixSelect = true;
    }
    private void OnMouseExit()
    {
        fixSelect = false;
        highlightShader.SetInt("_OutlineOn", 0);
        objectSelected = false;
    }

    //Stuff to avoid X-Ray selection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Selectable")
        {
            avoidDelete = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Selectable")
        {
            avoidDelete = false;
        }
    }
}