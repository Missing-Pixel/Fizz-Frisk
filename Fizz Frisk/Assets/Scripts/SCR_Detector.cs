using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Detector : MonoBehaviour
{
    SC_CanMovement deletion = null;
    SCR_Health healthManagerScript;
    GameObject healthManager;

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
            healthManagerScript = healthManager.GetComponent<SCR_Health>();
            healthManagerScript.PlayerDamaged();
        }
        else if (collision.tag == "Safe")
        {
            Debug.Log("Good Went Through");
        }

        //Tells the can to delete itself
        deletion = collision.gameObject.GetComponent<SC_CanMovement>();
        deletion.deleteSelf = true;
    }
}