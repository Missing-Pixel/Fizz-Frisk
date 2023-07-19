using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Detector : MonoBehaviour
{
    SC_CanMovement deletion = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Danger")
        {
            Debug.Log("Danger");
        }

        deletion = collision.gameObject.GetComponent<SC_CanMovement>();
        deletion.deleteSelf = true;
    }
}