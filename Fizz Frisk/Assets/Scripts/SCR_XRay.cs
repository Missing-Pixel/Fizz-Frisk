using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SCR_XRay : MonoBehaviour
{
    [HideInInspector] public GameObject selectedObj = null;
    Vector3 offset;
    private Color startcolor;

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetPos = Physics2D.OverlapPoint(mousePos);

            if (targetPos)
            {
                selectedObj = targetPos.transform.gameObject;
                offset = selectedObj.transform.position - mousePos;
            }
        } 
        else if (Input.GetMouseButtonUp(0) && selectedObj && selectedObj.CompareTag("Selectable"))
        {
            selectedObj = null;
            Debug.Log(" not selected");
        }

        if (selectedObj && selectedObj.CompareTag("Selectable"))
        {
            
            selectedObj.transform.position = mousePos + offset;
            Debug.Log("selected");
        }
    }

    private void OnMouseEnter()
    {
       // startcolor = GetComponent<Renderer>().material.color;
       // GetComponent<Renderer>().material.color = Color.white;
    }
    private void OnMouseExit()
    {
       // GetComponent<Renderer>().material.color = startcolor;
    }
}
