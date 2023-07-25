using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class SCR_XRay : MonoBehaviour
{
    [HideInInspector] public GameObject selectedObj = null;
    Vector3 offset;

    private void Update()
    {
        //Drag object with mouse
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

    //Clamp Movement
    private void LateUpdate()
    {
        var bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));

        var cameraRect = new Rect(
            bottomLeft.x,
            bottomLeft.y,
            topRight.x - bottomLeft.x,
            topRight.y - bottomLeft.y);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, cameraRect.xMin, cameraRect.xMax),
        Mathf.Clamp(transform.position.y, cameraRect.yMin, cameraRect.yMax), transform.position.z);

    }

    //Selection Shader
    private void OnMouseEnter()
    {
        //highlightShader.SetInt("_OutlineOn", 1);
    }
    private void OnMouseExit()
    {
        //highlightShader.SetInt("_OutlineOn", 0);
    }
}
