using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_Heart : MonoBehaviour
{
    public Sprite full, empty;
    Image heartImage;

    void Awake()
    {
        heartImage = GetComponent<Image>();
    }

    public void SetHeartImage(HeartStatus status)
    {
        switch(status)
        {
            case HeartStatus.Empty:
                heartImage.sprite = empty;
                break;
            case HeartStatus.Full:
                heartImage.sprite = full;
                break;
        }
    }
}

public enum HeartStatus
{
    Empty = 0,
    Full = 1
}