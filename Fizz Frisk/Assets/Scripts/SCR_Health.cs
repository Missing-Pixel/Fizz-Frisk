using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_Health : MonoBehaviour
{
    public int health;
    public int healthMax;
    public static event Action onPlayerDamaged;

    void Start()
    {
        health = healthMax;

        FindObjectOfType<SCR_AudioManager>().PlaySounds("BackgroundMusic");
    }

    public void PlayerDamaged()
    {
        health -= 1;
        Debug.Log("Running Multiple?");
        onPlayerDamaged.Invoke();

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
