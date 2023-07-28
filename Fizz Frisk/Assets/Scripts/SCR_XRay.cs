using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class SCR_XRay : MonoBehaviour
{
    [HideInInspector] public GameObject selectedObj = null;
    Vector3 offset;
    private Vector3 restPos;
    public Animator anim;
    Material highlightShader;
    public float batteryInterval = 3f;
    public float batteryTimer = 0f;
    private bool forcedRecharge = false;

    private void Awake()
    {
        highlightShader = GetComponent<SpriteRenderer>().material;
        restPos = transform.position;
    }

    private void Update()
    {
        var isOn = anim.GetBool("IsOn");
        var tabletState = anim.GetInteger("TabletState");

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
            transform.position = restPos;
            Debug.Log(" not selected");
            anim.SetBool("IsOn", false);
            FindObjectOfType<SCR_AudioManager>().StopSounds("Laser");
        }

        if (selectedObj && selectedObj.CompareTag("Selectable"))
        {
            if (Input.GetMouseButtonDown(0) && forcedRecharge == false)
            {
                anim.SetBool("IsOn", true);
                FindObjectOfType<SCR_AudioManager>().PlaySounds("Laser");
            }
            
            selectedObj.transform.position = mousePos + offset;
            Debug.Log("selected");
        }

        // Tablet States
        if (isOn == true && tabletState < 3)
        {
            if (batteryTimer < batteryInterval && forcedRecharge == false)
            {
                batteryTimer += (batteryInterval / 3) * Time.deltaTime;

                if (batteryTimer > batteryInterval / 3 && batteryTimer < (batteryInterval / 3) * 2)
                {
                    anim.SetInteger("TabletState", 1);
                }
                else if (batteryTimer > (batteryInterval / 3) * 2 && batteryTimer < (batteryInterval / 3) * 3)
                {
                    anim.SetInteger("TabletState", 2);
                }
                else if (batteryTimer >= batteryInterval)
                {
                    anim.SetInteger("TabletState", 4);
                    forcedRecharge = true;
                    FindObjectOfType<SCR_AudioManager>().PlaySounds("ShortCircuit");

                    if (Input.GetMouseButton(0))
                    {
                        anim.SetBool("IsOn", false);
                        FindObjectOfType<SCR_AudioManager>().StopSounds("Laser");
                    }
                }
            }
        }
        else if (isOn == false && tabletState > 0)
        {
            if (batteryTimer > 0)
            {
                batteryTimer -= (batteryInterval / 3) * Time.deltaTime;

                if (batteryTimer > (batteryInterval / 3) * 2)
                {
                    anim.SetInteger("TabletState", 3);
                }
                else if (batteryTimer < (batteryInterval / 3) * 2 && batteryTimer > batteryInterval / 3)
                {
                    anim.SetInteger("TabletState", 2);
                }
                else if (batteryTimer < batteryInterval / 3 && batteryTimer > 0)
                {
                    anim.SetInteger("TabletState", 1);
                }
            }
            else if (batteryTimer <= 0)
            {
                batteryTimer = 0;
                anim.SetInteger("TabletState", 0);
                forcedRecharge = false;

                if (Input.GetMouseButton(0))
                {
                    anim.SetBool("IsOn", true);
                    FindObjectOfType<SCR_AudioManager>().PlaySounds("Laser");
                }
            }
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
        highlightShader.SetInt("_OutlineOn", 1);
    }
    private void OnMouseExit()
    {
        highlightShader.SetInt("_OutlineOn", 0);
    }
}