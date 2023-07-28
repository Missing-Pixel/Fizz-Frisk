using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Detector : MonoBehaviour
{
    SC_CanMovement deletion = null;
    SCR_Health healthManagerScript;
    GameObject healthManager;
    public Animator anim;
    private SCR_Score scoreUpdate;
    public GameObject scoreManager;

    private void Start()
    {
        healthManager = GameObject.Find("OBJ_HealthManager");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Sends an output for if a good or bad can reaches the end
        if (collision.tag == "Danger")
        {
            Debug.Log("Bad Went Through");
            anim.SetInteger("ScanState", 2);
            FindObjectOfType<SCR_AudioManager>().PlaySounds("Buzz");
            healthManagerScript = healthManager.GetComponent<SCR_Health>();
            healthManagerScript.PlayerDamaged();
            Invoke("ResetState",1);
        }
        else if (collision.tag == "Safe")
        {
            Debug.Log("Good Went Through");
            anim.SetInteger("ScanState", 1);
            FindObjectOfType<SCR_AudioManager>().PlaySounds("Ding");
            Invoke("ResetState", 1);
            scoreUpdate = scoreManager.GetComponent<SCR_Score>();
            scoreUpdate.ScoreUpdate(100);
        }

        //Tells the can to delete itself
        deletion = collision.gameObject.GetComponent<SC_CanMovement>();
        deletion.deleteSelf = true;
    }

    void ResetState()
    {
        anim.SetInteger("ScanState", 0);
    }

}